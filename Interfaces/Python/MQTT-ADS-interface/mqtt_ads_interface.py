# -*- coding: utf-8 -*-
"""
Created on Tue Feb 23 12:42:53 2021

@author: Markus Schraven
"""

import mqtt as mqtt_module
import ads as ads_module
import time
from datetime import datetime
import parsing_and_assignment
import pytz

#%%
class mqtt_ads_interface():
    """
    A class used to create an mqtt and ads instance to manage transferring 
    data between them

    Attributes
    ----------
    mqtt : mqtt class object
        an instance of the mqtt class which covers generating an mqtt client 
        object, establishing the connection to a broker and handling publishing 
        and receiving messages from and to a broker.
    ads : ads class object
        an instance of the ads class which covers generating a plc object, 
        opening the port connection to the plc and handling reading and writing 
        of variables from and to the plc application.

    Methods
    -------
    connect_mqtt(mqtt_host='localhost', mqtt_port=1883, mqtt_keepalive=60, 
                 mqtt_user=None, mqtt_pass=None, credentials_env=None, aedifion=False)
        Invokes the connect function of the mqtt instance; the on_message 
        function of the mqtt instance is overwritten to define handling received 
        messages by a method in this class.
    disconnect_mqtt()
        Releases the mqtt connection to the broker.
    connect_ads(ams_netID, host, create_route=False)
        Invokes the connect function of the ads instance.
    disconnect_ads()
        Releases the ads connection to the plc.
    start_mqtt()
        Invokes the mqtt instance start function. This creates the mqtt threading 
        enabling listening for received messages. Afterwards the public method 
        of this class is called.
    listen(client=None, userdata=None, msg=None, sub_format='simple_json')
        Overrides the on_message function for received mnessages from the broker 
        and handles parsing and transferring the relevant data to the ads end point
    publish(pub_format='simple_json')
        Loops through a list of observations to be published and handles parsing 
        and transferring the relevant data to the mqtt end point
    """
#%%    
    def __init__(self):
        """"
        Class object initialization
        
        Instantiates MQTT and ADS class objects
        """
        self.mqtt = mqtt_module.mqtt()
        self.ads = ads_module.ads()
    
    def connect_mqtt(self, mqtt_host='localhost', mqtt_port=1883, mqtt_keepalive=60, 
                     mqtt_user=None, mqtt_pass=None, credentials_env=None, aedifion=False):
        """Invokes the connect function of the mqtt instance; the on_message 
        function of the mqtt instance is overwritten to define handling of received 
        messages by the listen method in this class.

        If the argument 'mqtt_host', 'mqtt_port' and 'mqtt_keepalive' are not 
        passed in, default parameters for a local connection are used. This 
        connection will require a local mqtt broker to be running when executed.

        Parameters
        ----------
        mqtt_host : str, optional
            The mqtt host/broker to connect to, usually an ip address or url-like 
            (default is 'localhost' or '127.0.0.1' respectively)
        mqtt_port : int, optional
            The port number on which a connection to the broker is running 
            (default for the local connection is 1883)
        mqtt_keepalive : int, optional
            The time in seconds that the mqtt connection to the broker is kept 
            alive while no traffic is registered in either sending or receiving direction
            (deault is 60)
        mqtt_user : str, optional
        mqtt_pass : str, optional
        credentials_env : tuple of str => ('evironment_user','environment_password'), optional
        aedifion : bool, optional
        """
        """
        1. Set username and password from a given tuple/array or from your system environment variables
        """
        if mqtt_user and mqtt_pass: # Set username and password from given credentials
            self.mqtt.set_credentials(mqtt_user, mqtt_pass)
        else: # Potentially use environment variables
            if aedifion == True: # Use aedifion environment variables
                self.mqtt.get_credentials(mqtt_user_environment_variable='AED_USER_MQTT',
                                          mqtt_password_environment_variable='AED_PASSWORD_MQTT')
            elif credentials_env: # Use user-specific environment variables from given tuple or array
                self.mqtt.get_credentials(mqtt_user_environment_variable=credentials_env[0],
                                          mqtt_password_environment_variable=credentials_env[1])
            else: # Use no credentials at all
                pass
        """
        2. Establish connection with broker
        """
        if aedifion == True:
            self.mqtt.connect_aedifion()
        else:
            self.mqtt.connect(host=mqtt_host, port=mqtt_port, keepalive=mqtt_keepalive)
        """
        3. Override on_message of mqtt instance
        """
        self.mqtt.on_message = self.listen
        self.mqtt.client.on_message = self.mqtt.on_message

    def disconnect_mqtt(self):
        self.mqtt.disconnect()    

    def connect_ads(self, ams_netID, host, create_route=False):
        if create_route == True:
            self.ads.create_route()
        self.ads.connect(ams_netID, host)
    
    def disconnect_ads(self):
        self.ads.disconnect()

#%%        
    def start_mqtt(self, publish_delay=None):
        self.mqtt.start_mqtt()
        self.publish(publish_delay=publish_delay)
        
    def listen(self, client=None, userdata=None, msg=None, sub_format='simple_json'):
        """"
        1. Listen for MQTT messages from cloud to write PLC data points
        2. Parse MQTT message to write PLC data point
        3. Send ADS command to PLC data point
        """
        msg.payload = msg.payload.decode("utf-8")  # All mqtt-topics are coded in utf-8
        print("Received messagae on topic " + msg.topic+" = "+str(msg.payload))
        if sub_format == 'simple_json':
            name, value = parsing_and_assignment.parseJSONsubscribe(msg.payload)
            # print(name+' '+str(value)+' '+str(type(value)))
            if name in sub:
                self.ads.write(name, value, sub[name])
                # Mirror written value
                self.mqtt.publish(message=msg.payload, topic='general')
    
    def publish(self, pub_format='simple_json', publish_delay=None):
        """"
        Loop through PLC data points to send
        1. Get data point to send from PLC
        2. Parse data point to send to MQTT message
        3. Publish MQTT message
        """
        while True:
            for i in pub:
                value = self.ads.read(i, pub[i][1])
                # value = self.ads.read('sampleADSGVL.'+i[0])
                if pub_format == 'simple_json':
                    timestamp = pytz.timezone('Europe/Berlin').localize(datetime.today()).isoformat(sep='T', timespec='milliseconds')
                    json = parsing_and_assignment.parseJSONpublish(i, value, timestamp)
                    # print(json)
                    self.mqtt.publish(message=json, topic='general')
            # time.sleep(1)
            if publish_delay:
                time.sleep(publish_delay)
        
#%%
if __name__ == "__main__":
    # Create ads and mqtt instances
    mqtt_ads = mqtt_ads_interface()
    # Connect MQTT
    try:
        mqtt_ads.connect_mqtt('localhost', 1883, 60)
    except:
        print('\n ****************************************** \n Could not create MQTT connection to broker. \n ****************************************** \n')
    # Connect ADS
    try:
        mqtt_ads.connect_ads(ams_netID='192.168.0.2.1.1', host='192.168.0.2')
    except:
        print('\n ****************************************** \n Could not create ADS connection to target system. \n ****************************************** \n')
    
    # Variables and Parsing
    # Get ADS variables from variable list
    # pub, sub = parsing_and_assignment.getADSVariables(file='TwinCAT Project1/TwinCAT Project1/Untitled1/GVLs/sampleADSGVL.TcGVL')
    pub, sub = parsing_and_assignment.getADSvarsFromSymbols(mqtt_ads.ads)
    try:
        mqtt_ads.start_mqtt(publish_delay=1)
    except KeyboardInterrupt:
        print("Disconnect MQTT..")
        mqtt_ads.disconnect_mqtt()
        print("MQTT was disconnected")
        print('Disconnect ADS..')
        mqtt_ads.disconnect_ads()
        print('ADS was disconnected')