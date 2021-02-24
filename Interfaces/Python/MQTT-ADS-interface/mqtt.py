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
    
    def __init__(self, port=8883):
        self.port = port
    
    def get_credentials(self, 
                        mqtt_user_environment_variable="mqtt_user_environment_variable", 
                        mqtt_password_environment_variable="mqtt_password_environment_variable"):
        """ Get MQTT credentials from environment variables """
        self.mqtt_username = os.getenv(mqtt_user_environment_variable)
        self.mqtt_password = os.getenv(mqtt_password_environment_variable)
    
    def set_host(self, host="mqtt.ercebc.aedifion.io"):
        """" Set MQTT host address """
        self.host = host
    
    def connect(self):
        self.client = PahoMQTTClient(client_id=f"{self.mqtt_username}-{str(time.localtime())}", protocol=MQTTv31, clean_session=True)
    
    def connect_aedifion(self, project="NextGenBAT"):
        self.client = PahoMQTTClient(client_id=f"{self.project}-{str(time.localtime())}", protocol=MQTTv31, clean_session=True)