# -*- coding: utf-8 -*-

import machine
import time
from parameters import par
from uModBusSerial import uModBusSerial

class board():
    
    def __init__(self):
        print("Setting properties.")
        # store and initialize I/O analogue pins
        self.pin_V_in       = par["pin_V_in"]
        self.V_in           = machine.ADC(self.pin_V_in)
        self.pin_V_out      = par["pin_V_out"]
        self.V_out          = machine.DAC(self.pin_V_out)
        self.pin_I_in       = par["pin_I_in"]
        self.I_in           = machine.ADC(self.pin_I_in)
        self.pin_I_out      = par["pin_I_out"]
        self.I_out          = machine.DAC(self.pin_I_out)
        
        # reference voltage
        # pins
        self.vref           = par["vref"]
        self.vref_1_3       = par["vref_1/3"]
        self.vref_2_3       = par["vref_2/3"]
        self.vref_dac_cal   = par["vref_dac_cal"]
        # voltage set values
        self.vref_1_3_set   = par["vref_1/3_setVoltage"]
        self.vref_2_3_set   = par["vref_2/3_setVoltage"]
        # digital set values
        self.dref_1_3_set   = par["dref_1_3_setDigital"]
        self.dref_2_3_set   = par["dref_2_3_setDigital"]
        # default dac
        self.adc_slope      = par["adc_slope"]
        self.adc_intercept  = par["adc_intercept"]
        # default dac
        self.dac_max_V_design   = par["dac_max_V_design"]
        self.dac_max_dig        = par["dac_max_dig"]
        
        # modbus related parameters
        self.slave_address  = par["modbus"]["address"]
        self.tx             = par["modbus"]["tx"]
        self.rx             = par["modbus"]["rx"]
        self.baudrate       = par["modbus"]["baud"]
        self.databits       = par["modbus"]["bits"]
        self.stopbits       = par["modbus"]["stop"]
        self.parity         = par["modbus"]["parity"]
        self.uartTimeout    = par["modbus"]["timeout"]
        self.control_pin    = par["modbus"]["control_pin"]
        self.available_regs = par["modbus"]["available_regs"]
        # initialize modbus device over UART
        self.modbus = uModBusSerial(slave_address=self.slave_address, baudrate=self.baudrate, data_bits=self.databits, 
                                    stop_bits=self.stopbits, parity=self.parity, timeout=self.uartTimeout, 
                                    ctrl_pin=self.control_pin, pins=[self.tx,self.rx])

        # conversion parameters
        # 0..10 V voltage divider
        self.V_in_R1        = par["0..10V_in"]["R1"]
        self.V_in_R2        = par["0..10V_in"]["R2"]
        # 0..10 V OP amplifier
        self.V_out_R1       = par["0..10V_out"]["R1"]
        self.V_out_R2       = par["0..10V_out"]["R2"]
        # 0..20 V current loop input
        self.I_in_R1        = par["0..20mA_in"]["R1"]
        # 0..20 V current loop output
        self.I_out_R1       = par["0..20mA_out"]["R1"]
        
        # set reference voltage and calibrate ADC as well as DAC
        self.set_ESP_referenceVoltage()
        self.calibrate()
    
    #%% assign reference voltage to I/O    
    def set_ESP_referenceVoltage(self):
        print("Setting internal reference voltage.")
        machine.ADC.vref(vref_topin=self.vref)
        time.sleep(0.5)
    
    #%% calibrate adc and dac
    def calibrate(self, adc_flag="default", dac_flag="default"):
        # adc calibration
        if adc_flag == "default":
            self.adc_line_parameters = self.set_adc_default_regression()
        else:
            self.adc_line_parameters = self.calibrate_adc()
        # dac calibration
        if dac_flag == "default":
            self.dac_line_parameters = self.set_dac_default_regression()
        else:
            self.dac_line_parameters = self.calibrate_dac()
    
    #%% adc calibration
    def calibrate_adc(self):
        
        print("Calibrating ADC ...")
        time.sleep(0.5)
        # initialize pins
        adc_1_3 = machine.ADC(self.vref_1_3)
        adc_2_3 = machine.ADC(self.vref_2_3)
        
        # loop and calculate mean
        temp_1_3 = []
        temp_2_3 = []
        for i in range(1000):
            # read digital values for 1/3 and 2/3 of the reference voltage
            temp_1_3.append(adc_1_3.readraw()) # digital value related to 1/3 of the reference voltage (self.vref_1_3_set)
            temp_2_3.append(adc_2_3.readraw()) # digital value related to 2/3 of the reference voltage (self.vref_2_3_set)
        print(temp_1_3)
        print(temp_2_3)
        d_1_3 = sum(temp_1_3)/len(temp_1_3)
        d_2_3 = sum(temp_2_3)/len(temp_2_3)
        # actually expected voltage values
        v_1_3 = self.vref_1_3_set
        v_2_3 = self.vref_2_3_set
        
        # calculate linear regression parameters y = m*x + n where x is a digital value and y a voltage
        m = (v_2_3 - v_1_3)/(d_2_3 - d_1_3)
        n = v_2_3 - m*d_2_3
        
        print ("v_1_3=%f, v_2_3=%f, d_1_3=%f, d_2_3=%f "%(v_1_3,v_2_3,d_1_3,d_2_3))
        print ("m=%f, n=%f "%(m,n))
        print("ADC calibrated. \n ") 
        
        # deinitialize pins
        adc_1_3.deinit()
        adc_2_3.deinit()
        
        time.sleep(0.5)
        
        adc_line_parameters = [m,n]
        
        return adc_line_parameters
    
    #%% dac calibration
    def calibrate_dac(self):
        
        print("Calibrating DAC ...")
        time.sleep(0.5)
        # initialize pins
        adc = machine.ADC(self.vref_dac_cal)
        #dac = machine.DAC(self.pin_I_out)
        dac = self.I_out
        # predefined digital values written to DAC (1/3 and 2/3 of the max range)
        d_1_3 = self.dref_1_3_set
        d_2_3 = self.dref_2_3_set
        # write and read
        dac.write(int(round(d_1_3)))
        time.sleep(0.5)
        v_1_3 = self.adc_dig_to_volt(adc.readraw())
        dac.write(int(round(d_2_3)))
        time.sleep(0.5)
        v_2_3 = self.adc_dig_to_volt(adc.readraw())
        
        # calculate linear regression parameters y = m*x + n where x is a digital value and y a voltage
        m = (v_2_3 - v_1_3)/(d_2_3 - d_1_3)
        n = v_2_3 - m*d_2_3
        
        print ("v_1_3=%f, v_2_3=%f, d_1_3=%f, d_2_3=%f "%(v_1_3,v_2_3,d_1_3,d_2_3))
        print ("m=%f, n=%f "%(m,n))
        print("DAC calibrated. \n ") 
        
        time.sleep(0.5)
        
        # reset dac
        dac.write(0)
        
        time.sleep(0.5)
        
        dac_line_parameters = [m,n]
        
        return dac_line_parameters
    
    def set_adc_default_regression(self):
        
        print("Setting default DAC conversion..")
        
        m = self.adc_slope
        n = self.adc_intercept
        
        print ("m=%f, n=%f "%(m,n))
        print("ADC set up. \n ")
        
        return [m,n]
    
    def set_dac_default_regression(self):
        
        print("Setting default DAC conversion..")
        
        m = self.dac_max_V_design/self.dac_max_dig
        n = 0
        
        print ("m=%f, n=%f "%(m,n))
        print("DAC set up. \n ")
        
        return [m,n]
    
    #%% digital signal conversions
    # adc conversion digital value to voltage
    def adc_dig_to_volt(self, d):
        
        v = self.adc_line_parameters[0]*d + self.adc_line_parameters[1]
        
        return v
    
    # adc conversion voltage to digital value
    def adc_volt_to_dig(self, v):
        
        d = (v - self.adc_line_parameters[1])/self.adc_line_parameters[0]
        
        return d
    
    # dac conversion digital value to voltage
    def dac_dig_to_volt(self, d):
        
        v = self.dac_line_parameters[0]*d + self.dac_line_parameters[1]
        
        return v
    
    # dac conversion voltage to digital value
    def dac_volt_to_dig(self, v):
        
        d = (v - self.dac_line_parameters[1])/self.dac_line_parameters[0]
        
        return d
    
    #%% reading/writing I/O analogue functions
    # read 0..10 V input
    def read_voltage(self, read_V_flag="volt"):
        
        temp_d = []
        for i in range(1000):
            temp_d.append(self.V_in.readraw())
        d = sum(temp_d)/len(temp_d)
        del temp_d
        if read_V_flag == "volt":
            print("ADC input digital value: "+str(int(round(d)))+" of 3390.")
            print("ADC input voltage: "+str(self.adc_dig_to_volt(d))+" of 0.91 V.")
            v = self.adc_dig_to_volt(d)*(self.V_in_R1+self.V_in_R2)/self.V_in_R1
        else:
            v = d
        del d
        
        return v
    
    # write 0..10 V output
    def write_voltage(self, v):
        
#        if not isinstance(v,int) or not isinstance(v,float):
#            raise TypeError("Wrong type: Please input a number.")
#        elif v < 0 or v > 10:
#            raise IOError("Input out of range: Please enter a value between 0 and 10 V.")
#        else:
#            self.V_out.write(self.dac_volt_to_dig(int(round(v/(1+self.V_out_R1/self.V_out_R2)))))
        
        print("Write voltage of "+str(v)+" V.")
        print("DAC output voltage: "+str(v/(1+self.V_out_R1/self.V_out_R2))+" of 3.15 V.")
        print("DAC output digital value: "+str(int(round(self.dac_volt_to_dig((v/(1+self.V_out_R1/self.V_out_R2))))))+" of 255.")
        self.V_out.write(int(round(self.dac_volt_to_dig((v/(1+self.V_out_R1/self.V_out_R2))))))
    
    # read 0..20 mA input
    def read_current(self, read_I_flag="curr"):
        
        temp_d = []
        for j in range(1000):
            temp_d.append(self.I_in.readraw())
        d = sum(temp_d)/len(temp_d)
        del temp_d
        if read_I_flag == "curr":
            print("ADC input digital value: "+str(int(round(d)))+" of 3390.")
            print("ADC input voltage: "+str(self.adc_dig_to_volt(d))+" of 0.91 V.")
            i = self.adc_dig_to_volt(d)/self.I_in_R1*1000
        else:
            i = d
        del d
        
        return i
    
    # write 0..20 mA output
    def write_current(self, i):
        
#        if not isinstance(i,int) or not isinstance(i,float):
#            raise TypeError("Wrong type: Please input a number.")
#        elif i < 0 or i > 20:
#            raise IOError("Input out of range: Please enter a value between 0 and 10 V.")
#        else:
#            self.I_out.write(self.dac_volt_to_dig(int(round(i*self.I_out_R1/100))))
        
        # i is a value in mA
#        i = i/1000
#        print("Write current of "+str(i*1000)+" mA.")
#        print("DAC output voltage: "+str(i*self.I_out_R1/100)+" of 3.15 V.")
#        print("DAC output digital value: "+str(int(round(self.dac_volt_to_dig((i*self.I_out_R1/100)))))+" of 255.")
#        self.I_out.write(int(round(self.dac_volt_to_dig(i*self.I_out_R1/100))))
        
        # i is a value in mA
        # different calculation for new current loop setup: V = a * I^b
        v = 0.55246188*(pow(i,0.56551304))
        print("DAC output voltage: "+str(v))
        print("DAC output digital value: "+str(int(round(self.dac_volt_to_dig(v)))))
        self.I_out.write(int(round(self.dac_volt_to_dig(v))))

if __name__=="__main__":
    board = board