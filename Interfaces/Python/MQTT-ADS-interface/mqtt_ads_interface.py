# -*- coding: utf-8 -*-
"""
Created on Tue Feb 23 12:42:53 2021

@author: Markus Schraven
"""

from threading import Thread
import threading
import mqtt as mqtt_module
import ads as ads_module
import time
import parsing_and_assignment

#%%
class mqtt_ads_interface():
    
    def __init__(self):
        """"
        Instantiate MQTT and ADS class objects
        """
        self.mqtt = mqtt_module.mqtt()
        self.ads = ads_module.ads()
    
    def connect_mqtt(self, aedifion=True):
        """"
        Establish connection to MQTT broker:
        1. Set the host address of the broker
        2. Get username and password from your system environment variables
        """
        self.mqtt.set_host(host='mqtt.ercebc.aedifion.io')
        self.mqtt.get_credentials(mqtt_user_environment_variable='AED_USER_MQTT',
                                  mqtt_password_environment_variable='AED_PASSWORD_MQTT')
        if aedifion == True:
            self.mqtt.connect_aedifion()
        else:
            self.mqtt.connect()
    
    def connect_ads(self, create_route=False):
        if create_route == True:
            self.ads.create_route()
        self.ads.connect(ams_netID="5.53.34.234.1.1", host="134.130.56.144")
    
    def listen(self, threadname, termination):
        """"
        1. Listen for MQTT messages from cloud to write PLC data points
        2. Parse MQTT message to write PLC data point
        3. Send ADS command to PLC data point
        """
        i = 0 
        while termination.is_set():       
            print(f"thread {threadname} lives for {i} seconds\n")
            time.sleep(1)
            i += 1
    
    def publish(self, threadname, termination):
        """"
        Loop through PLC data points to send
        1. Get data point to send from PLC
        2. Parse data point to send to MQTT message
        3. Publish MQTT message
        """
        print(f"thread {threadname} sleeps for 8 seconds\n")
        i = 0
        temp = False
        while termination.is_set():
            if i < 8:
                time.sleep(1)
                i += 1
            elif temp == False:
                print(f"thread {threadname} woke up")
                temp = True
        
#%%
if __name__ == "__main__":
    # Create ads and mqtt instances
    mqtt_ads = mqtt_ads_interface()
    # Variables and Parsing
    pub, sub = parsing_and_assignment.getADSVariables()
    # Create termination event, e.g. keyboardinterrupt
    termination = threading.Event()
    termination.set()
    try:
        # Create two threads for MQTT listening and publishing
        listen = Thread(target=mqtt_ads.listen, args=("MQTT-listen",termination,))
        publish = Thread(target=mqtt_ads.publish, args=("MQTT-publish",termination,))
        try:
            listen.start()
            publish.start()
        except:
            print("Error: unable to start thread")
        while termination.is_set():
            # Keep main thread alive
            pass
    except KeyboardInterrupt:
        print("attempting to close threads.")
        termination.clear()
        listen.join()
        publish.join()
        print("threads successfully closed")