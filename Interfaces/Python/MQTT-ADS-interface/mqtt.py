# -*- coding: utf-8 -*-
"""
Created on Tue Feb 23 12:45:36 2021

@author: Markus Schraven
"""

from paho.mqtt.client import Client as PahoMQTTClient, MQTTv31
import os
import ssl
import time
import certifi
import random

#%%
class mqtt():
    
    def __init__(self, host=None, port=None, keepalive=None, mqtt_user=None, mqtt_password=None):
        self.host           = host
        self.port           = port
        self.keepalive      = keepalive
        self.mqtt_username  = mqtt_user
        self.mqtt_password  = mqtt_password

#%%
    # get credentials from environment variables
    def get_credentials(self, 
                        mqtt_user_environment_variable="mqtt_user_environment_variable", 
                        mqtt_password_environment_variable="mqtt_password_environment_variable"):
        """ Get MQTT credentials from environment variables """
        self.mqtt_username = os.getenv(mqtt_user_environment_variable)
        self.mqtt_password = os.getenv(mqtt_password_environment_variable)
    
    def set_credentials(self, user, password):
        self.mqtt_username = user
        self.mqtt_password = password
    
    def set_host(self, host):
        """" Set MQTT host address """
        self.host = host
    
    def set_port(self, port):
        self.port = port
    
    def set_keepalive(self, keepalive):
        self.keepalive = keepalive

#%%    
    def on_connect(self, client=None, userdata=None, flags=None, rc=None):
        if self.client.connected_flag == False:
            if client:
                self.client = client
            if rc == 0:
                self.client.connected_flag = True #set flag
                print("MQTT connection was established.")
            else:
                print("Bad connection, return code=",rc)
   
    def connect(self, client=None, host=None, port=None, keepalive=None, clientID=None):
        # Create MQTT client
        if client:
            self.client = client
        else:
            if not self.mqtt_username:
                self.mqtt_username = 'test-user'
            if clientID:
                self.client = PahoMQTTClient(client_id=clientID, protocol=MQTTv31, clean_session=True)
            else:
                clienttime = time.strftime('%Y-%m-%dT%H:%M:%S', time.localtime())
                self.client = PahoMQTTClient(client_id=f"{self.mqtt_username}-{clienttime}", protocol=MQTTv31, clean_session=True)
        self.client.connected_flag = False # Create connection flag
        self.client.on_connect = self.on_connect  # Bind call back function
        # Get host, port and keepalive
        if host:
            self.host = host
        elif not self.host:
            self.host = 'localhost'
        if port:
            self.port = port
        elif not self.port:
            self.port = 1883
        if keepalive:
            self.keepalive = keepalive
        elif not self.keepalive:
            self.keepalive = 60
        # Establish connection of client to broker
        try:
            self.client.username_pw_set(self.mqtt_username, self.mqtt_password)
            self.client.connect(self.host, self.port, self.keepalive)
        except:
            print('Could not establish connection to the broker with host '+self.host+'\n')            
    
    def connect_aedifion(self, project=None, host=None, port=8883):
        self.client = PahoMQTTClient(client_id=f"{project}-{str(time.localtime())}", protocol=MQTTv31, clean_session=True)
        self.client.tls_set(ca_certs=certifi.where(), tls_version=ssl.PROTOCOL_TLSv1_2)
        self.client.username_pw_set(self.mqtt_username, self.mqtt_password)
        self.client.on_connect = self.on_connect  # Bind call back function
        # Establish connection to aedifion broker
        try:
            self.client.connect(host=host, port=port)
        except:
            print('Could not establish connection to the aedifion broker with host '+host)
    
    def disconnect(self, client=None):
        if client:
            client.disconnect()
        else:
            self.client.disconnect()
    
#%%
    def start_mqtt(self):
        self.client.on_message = self.on_message # Bind call back function
        self.client.on_publish = self.on_publish # Bind call back function
        self.client.loop_start()
        self.client.subscribe('controls')

    def on_message(self, client=None, userdata=None, msg=None):
        msg.payload = msg.payload.decode("utf-8")  # All mqtt-topics are coded in utf-8
        print("Received messagae on topic " + msg.topic+" = "+str(msg.payload))

    def on_publish(self, client, userdata, msg_id):
        pass
    
    def publish(self, message, topic, client=None):
        self.client.publish(topic, message, qos=1)