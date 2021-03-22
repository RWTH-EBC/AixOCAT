# -*- coding: utf-8 -*-
"""
Created on Tue Feb 23 12:45:36 2021

@author: Markus Schraven
"""

from paho.mqtt.client import Client as PahoMQTTClient, MQTTv31
import os
import ssl
import time

#%%
class mqtt():
    
    def __init__(self, host=None, port=None, keepalive=None, mqtt_user=None, mqtt_password=None):
        self.host = host
        self.port = port
        self.keepalive = keepalive
        self.mqtt_username = mqtt_user
        self.mqtt_password = mqtt_password

#%%
    # get credentials from environment variables
    def get_credentials(self, 
                        mqtt_user_environment_variable="mqtt_user_environment_variable", 
                        mqtt_password_environment_variable="mqtt_password_environment_variable"):
        """ Get MQTT credentials from environment variables """
        self.mqtt_username = os.getenv(mqtt_user_environment_variable)
        self.mqtt_password = os.getenv(mqtt_password_environment_variable)
    
    def set_host(self, host="mqtt.ercebc.aedifion.io"):
        """" Set MQTT host address """
        self.host = host
    
    def set_port(self, port=8883):
        self.port = port
    
    def set_keepalive(self, keepalive=60):
        self.keepalive = keepalive

#%%    
    def on_connect(self, client=None, userdata=None, flags=None, rc=None):
        if not client:
            client = self.client
        if rc == 0:
            client.connected_flag = True #set flag
            print("MQTT connection was established.")
        else:
            print("Bad connection, return code=",rc)
   
    def connect(self, client=None, host=None, port=None, keepalive=None, clientID=None):
        if not client:
            if not self.mqtt_username:
                self.mqtt_username = 'test-user'
            if clientID:
                self.client = PahoMQTTClient(client_id=clientID, protocol=MQTTv31, clean_session=True)
            else:
                self.client = PahoMQTTClient(client_id=f"{self.mqtt_username}", protocol=MQTTv31, clean_session=True)
            self.client.connected_flag = False #create connection flag
            self.client.on_connect = self.on_connect  #bind call back function
        if not host:
            if self.host:
                host = self.host
            else:
                host = 'localhost'
        if not port:
            if self.port:
                port = self.port
            else:
                port = 1883
        if not keepalive:
            if self.keepalive:
                keepalive = self.keepalive
            else:
                keepalive = 60
        if not client:
            self.client.connect(host, port, keepalive)
        else:
            client.connected_flag = False #create connection flag
            client.on_connect = self.on_connect  #bind call back function
            client.connect(host, port, keepalive)
    
    def connect_aedifion(self, project="NextGenBAT"):
        self.client = PahoMQTTClient(client_id=f"{self.project}-{str(time.localtime())}", protocol=MQTTv31, clean_session=True)
    
    def disconnect(self, client=None):
        if not client:
            client = self.client       
        client.disconnect()
    
#%%
    def on_publish(self, client, userdata, mid):
        print("mid: "+str(mid))
    
    def publish(self, message, topic, client=None):
        self.client.publish(topic, message, qos=1)