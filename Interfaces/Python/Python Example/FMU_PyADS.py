
# Import the load function (load_fmu) and the plot funtion matplotlib.pyplot
from pyfmi import load_fmu
import matplotlib.pyplot as plt


#Load the FMU
model = load_fmu('AixoCatExample.fmu')

#initialize
model.initialize()

#save results in an array
Valve_Opening=[]


import pyads

# Connect to TwinCat 3 as local host
plc = pyads.Connection('127.0.0.1.1.1', 851)  # SIL
plc.open()

#Simulate in ( ) steps
for i in range (8000):
    #get variable
    Valve_Opening.append(model.get('ValveOpening'))
    
    print('Step: ' + str(i + 1), 'Valve_Opening=', Valve_Opening[i])
    
    #Send the Results to TwinCAT
    plc.write_by_name("GVL.ValveOpening",Valve_Opening[i], pyads.PLCTYPE_REAL)
    
    #jump to next step
    model.do_step(i, 1, True)
    #time.sleep(0.5)


#This section writes the data into a txt file which will be saved in the same directory
with open('output.txt', 'w') as f:
    for item in Valve_Opening:
        f.write("%s\n" % item)

#This section plots the results as graph
plt.title('Final Air Valve Regulation')
plt.xlabel('Time in s')
plt.ylabel('Valve Opening')
plt.plot(Valve_Opening)
plt.show()

