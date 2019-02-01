# -*- coding: utf-8 -*-

par = {}

#%% modbus parameters
par["modbus"]                   = {}
par["modbus"]["address"]        = 1 # modbus slave address
par["modbus"]["tx"]             = 10 # tx pin of the ESP
par["modbus"]["rx"]             = 9 # rx pin of the ESP
par["modbus"]["baud"]           = 38400 # modbus slave baudrate
par["modbus"]["bits"]           = 8
par["modbus"]["stop"]           = 2
par["modbus"]["parity"]         = None
par["modbus"]["timeout"]        = 10
par["modbus"]["control_pin"]    = None # automatic flow control => no control pin
par["modbus"]["available_regs"] = [0,1,2,3,4,5,8,12,99,100,101,102,103,
                                   104,105,106,107,108,109,118]
#%% I/O pins
par["pin_V_in"]                 = 37 # 0..10 V sensing
par["pin_V_out"]                = 26 # 0..10 V control
par["pin_I_in"]                 = 38 # 0..20 mA sensing
par["pin_I_out"]                = 25 # 0..20 mA control

#%% reference voltage
# pins
par["vref"]                     = 27 # reference voltage of the ESP; standard voltage is 1100 mV
par["vref_1/3"]                 = 33 # ADC pin reading 1/3 of the reference voltage
par["vref_2/3"]                 = 32 # ADC pin reading 2/3 of the reference voltage
par["vref_dac_cal"]             = 39 # ADC pin reading given voltage by DAC (pin_I_out)
# predefined values
# use standard 1100 mV reference voltage
par["vref_1/3_setVoltage"]      = float(1.1/3)
par["vref_2/3_setVoltage"]      = float(1.1/3*2)
# use standard 8 bit resolution DAC
par["dref_1_3_setDigital"]      = float((255/3)/3)
par["dref_2_3_setDigital"]      = float((255/3)/3*2)

#%% conversions
par["0..10V_in"]                = {}
par["0..10V_in"]["R1"]          = 3.3e3 # Ohm
par["0..10V_in"]["R2"]          = 33e3  # Ohm
par["0..10V_out"]               = {}
par["0..10V_out"]["R1"]         = 22e3  # Ohm
par["0..10V_out"]["R2"]         = 10e3  # Ohm
par["0..20mA_in"]               = {}
par["0..20mA_in"]["R1"]         = 45.3  # Ohm
par["0..20mA_out"]              = {}
par["0..20mA_out"]["R1"]        = 15000 # Ohm

par["dac_max_V_design"]         = 3.2   # V
par["dac_max_dig"]              = 255   # digital value
par["adc_slope"]                = 243e-6
par["adc_intercept"]            = 0.0649