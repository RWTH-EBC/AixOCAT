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
from configparser import ConfigParser
import ast

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
                     mqtt_user=None, mqtt_pass=None, credentials_env=None, aedifion=None,
                     pub_wo_top={}, sub_wo_top={}):
        """Invokes the connect function of the mqtt instance; the on_message 
        function of the mqtt instance is overwritten to define handling of received 
        messages by the listen method in this class.

        If the argument 'mqtt_host', 'mqtt_port' and 'mqtt_keepalive' are not 
        passed in, default parameters for a local connection are used. Therefore, this 
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
            The mqtt username which is used in authentication and as ID
        mqtt_pass : str, optional
            The mqtt password which is used in authentication
        credentials_env : tuple of str => ('evironment_user','environment_password'), optional
            A tuple of environment variable entries to fetch the mqtt username 
            and password from (default is None which means that the credentials 
            will not be fetched)
        aedifion : bool, optional
            A bool signalizing if the connected broker will be aedifion which
            results in specific treatment (e.g. fetching user and password from
            specific env entries and user different functions for the connection;
            default is False)
        """
        """
        1. Set username and password from a given tuple/array or from your system environment variables
        """
        if mqtt_user and mqtt_pass: # Set username and password from given credentials
            self.mqtt.set_credentials(mqtt_user, mqtt_pass)
        else: # Potentially use environment variables
            if aedifion: # Use aedifion environment variables
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
        if aedifion:
            self.mqtt.connect_aedifion(project=aedifion['project'], host=mqtt_host, port=mqtt_port)
        else:
            self.mqtt.connect(host=mqtt_host, port=mqtt_port, keepalive=mqtt_keepalive)
        """
        3. Assign topic and handle messages (e.g. override on_message of mqtt instance)
        """
        if aedifion:
            # Add topics
            pub = parsing_and_assignment.add_topics(pub_wo_top, pub_wo_top.keys(), aedifion['topic_prefix_publish'])
            sub = parsing_and_assignment.add_topics(sub_wo_top, sub_wo_top.keys(), aedifion['topic_prefix_subscribe'])
            # Subscribe to the entire controls topic
            self.mqtt.client.subscribe(f"{aedifion['topic_prefix_subscribe']}", qos=0)
            self.mqtt.on_message = self.listen
            self.mqtt.client.on_message = self.mqtt.on_message
        else:
            # Add topics
            pub = parsing_and_assignment.add_topics(pub_wo_top, pub_wo_top.keys(), 'general')
            sub = parsing_and_assignment.add_topics(sub_wo_top, sub_wo_top.keys(), 'controls')
            # Subscribe to controls topic
            self.mqtt.client.subscribe("controls", qos=0)
            self.mqtt.on_message = self.listen
            self.mqtt.client.on_message = self.mqtt.on_message
        return pub, sub

    def disconnect_mqtt(self):
        """Invokes the disconnect function of the mqtt instance and thus disconnects 
        the client from the broker.
        """
        self.mqtt.disconnect()    

    def connect_ads(self, ams_netID, host, ads_user, ads_password, create_route=False):
        if create_route == True:
            self.ads.create_route(ams_netID=ams_netID, remote_ip=host, user=ads_user, password=ads_password)
        self.ads.connect(ams_netID, host)
    
    def disconnect_ads(self):
        """Invokes the disconnect function of the ads instance and thus disconnects 
        the client from the plc.
        """
        self.ads.disconnect()

#%%        
    def start_mqtt(self, publish_format='simple_json', subscribe_format='simple_json', publish_delay=None):
        self.mqtt.start_mqtt()
        self.subscribe_format = subscribe_format
        self.publish(publish_format=publish_format, publish_delay=publish_delay)
        
    def listen(self, client=None, userdata=None, msg=None):
        """"
        1. Listen for MQTT messages from cloud to write PLC data points
        2. Parse MQTT message to write PLC data point
        3. Send ADS command to PLC data point
        """
        msg.payload = msg.payload.decode("utf-8")  # All mqtt-topics are coded in utf-8
        # print("Received messagae on topic " + msg.topic+" = "+str(msg.payload))
        if self.subscribe_format == 'simple_json':
            name, value = parsing_and_assignment.parseJSONsubscribe(msg.payload)
            # print(name+' '+str(value)+' '+str(type(value)))
            if name in sub:
                try:
                    self.ads.write(name, value, sub[name]['type'])
                    # Mirror written value
                    # self.mqtt.publish(message=msg.payload, topic='general')
                except KeyboardInterrupt:
                    pass
                except:
                    print('Something went wrong when trying to apply control on received message of '+name)
        elif self.subscribe_format == 'SWOP':
            name, temp1, temp2 = msg.payload.split(" ")[:3]
            temp2, value = temp1.split("=")[:2]
            print(name+' '+value)
    
    def publish(self, publish_format='simple_json', publish_delay=None):
        """"
        Loop through PLC data points to send
        1. Get data point to send from PLC
        2. Parse data point to send to MQTT message
        3. Publish MQTT message
        """
        while True:
            for i in pub:
                try:
                    value = self.ads.read(i, pub[i]['handle'])
                    if publish_format == 'simple_json':
                        timestamp = pytz.timezone('Europe/Berlin').localize(datetime.today()).isoformat(sep='T', timespec='milliseconds')
                        json = parsing_and_assignment.parseJSONpublish(i, value, timestamp)
                        # print(json)
                        self.mqtt.publish(message=json, topic=pub[i]['topic'])
                    if publish_format == 'influxDB_line':
                        timestamp = timestamp = int(time.time()*10**9)
                        influxDB_line = parsing_and_assignment.parseInfluxDBLinepublish(i, value, timestamp)
                        print(influxDB_line)
                        self.mqtt.publish(message=influxDB_line, topic=pub[i]['topic'])
                except KeyboardInterrupt:
                    break
                except:
                    print('Error in publishing '+i)
            # print('\n Reached the end of publishing list, restarting publishing from beginning. \n')
            if publish_delay:
                time.sleep(publish_delay)

#%%
def write_config(f='config.ini'):
    config = ConfigParser()
    config.read(f)
    if not config.has_section('mqtt'):
        config.add_section('mqtt')
    config.set('mqtt', 'host', 'localhost')
    config.set('mqtt', 'port', '1883')
    config.set('mqtt', 'keepalive', '60')
    config.set('mqtt', 'user', 'msh')
    config.set('mqtt', 'password', 'None')
    config.set('mqtt', 'credentials_env', 'None')
    config.set('mqtt', 'aedifion', 'None')
    config.set('mqtt', 'publish_delay', '1')
    config.set('mqtt', 'publish_encoding', 'simple_json')
    if not config.has_section('ads'):
        config.add_section('ads')
    config.set('ads', 'ams_netID', '192.168.0.2.1.1')
    config.set('ads', 'host', '192.168.0.2')
    config.set('ads', 'create_route', 'False')
    config.set('ads', 'remote_user', 'Administrator')
    config.set('ads', 'remote_password', 'MyPassword')
    
    with open('config.ini', 'w') as f:
        config.write(f)

def load_config(f='config.ini'):
    config = ConfigParser()
    config.read(f)
    config_dict = {}
    config_dict['mqtt_host'] = config.get('mqtt', 'host')
    config_dict['mqtt_port'] = config.getint('mqtt', 'port')
    config_dict['mqtt_keepalive'] = config.getint('mqtt', 'keepalive')
    config_dict['mqtt_user'] = config.get('mqtt', 'user')
    config_dict['mqtt_password'] = config.get('mqtt', 'password')
    config_dict['mqtt_credentials_env'] = config.get('mqtt', 'credentials_env')
    config_dict['mqtt_aedifion'] = config.get('mqtt', 'aedifion')
    config_dict['mqtt_publish_delay'] = config.getfloat('mqtt', 'publish_delay')
    config_dict['mqtt_publish_encoding'] = config.get('mqtt', 'publish_encoding')
    config_dict['mqtt_subscribe_decoding'] = config.get('mqtt', 'subscribe_decoding')
    config_dict['ads_ams_netID'] = config.get('ads', 'ams_netID')
    config_dict['ads_host'] = config.get('ads', 'host')
    config_dict['ads_create_route'] = config.getboolean('ads', 'create_route')
    config_dict['ads_remote_user'] = config.get('ads', 'remote_user')
    config_dict['ads_remote_password'] = config.get('ads', 'remote_password')
    return config_dict

#%%
if __name__ == "__main__":
    # TODO: load config file
    load_conf=True
    config = load_config(f='config.ini')
    
    #**************************************************************************
    # Create ads and mqtt instances
    mqtt_ads = mqtt_ads_interface()
    # Connect ADS
    try:
        if load_conf == True:
            mqtt_ads.connect_ads(ams_netID=config['ads_ams_netID'], host=config['ads_host'],
                                 create_route=config['ads_create_route'], 
                                 ads_user=config['ads_remote_user'], ads_password=config['ads_remote_password'])
        else:
            mqtt_ads.connect_ads(ams_netID='192.168.0.2.1.1', host='192.168.0.2', create_route=False)
    except:
        print('\n ****************************************** \n Could not create ADS connection to target system. \n ****************************************** \n')
        # Variables and Parsing
    # Get ADS variables from variable list
    # pub, sub = parsing_and_assignment.getADSVariables(file='TwinCAT Project1/TwinCAT Project1/Untitled1/GVLs/sampleADSGVL.TcGVL')
    pub, sub = parsing_and_assignment.getADSvarsFromSymbols(mqtt_ads.ads)
    # Connect MQTT
    try:
        if load_conf == True:
            if config['mqtt_aedifion'] == 'None':
                config['mqtt_aedifion'] = None
            else:
                config['mqtt_aedifion'] = ast.literal_eval(config['mqtt_aedifion'])
            pub, sub = mqtt_ads.connect_mqtt(mqtt_host=config['mqtt_host'], mqtt_port=config['mqtt_port'], 
                                  mqtt_keepalive=config['mqtt_keepalive'], aedifion=config['mqtt_aedifion'],
                                  pub_wo_top=pub, sub_wo_top=sub)
        else:
            pub, sub = mqtt_ads.connect_mqtt('localhost', 1883, 60, pub_wo_top=pub, sub_wo_top=sub)
    except:
        print('\n ****************************************** \n Could not create MQTT connection to broker. \n ****************************************** \n')
    #**************************************************************************
    
    # TODO: write config file
    # write_conf=True
    # write_config()
    
    try:
        if load_conf == True:
            mqtt_ads.start_mqtt(publish_format=config['mqtt_publish_encoding'], 
                                subscribe_format=config['mqtt_subscribe_decoding'], 
                                publish_delay=config['mqtt_publish_delay'])
        else:
            mqtt_ads.start_mqtt(publish_delay=1)
    except KeyboardInterrupt:
        print("Disconnect MQTT..")
        mqtt_ads.disconnect_mqtt()
        print("MQTT was disconnected")
        print('Disconnect ADS..')
        print('Release handles')
        for i in pub:
            mqtt_ads.ads.plc.release_handle(pub[i]['handle'])
        for i in sub:
            mqtt_ads.ads.plc.release_handle(sub[i]['handle'])
        mqtt_ads.disconnect_ads()
        print('ADS was disconnected')
    except:
        print("Faced an unintentional error, closing connections for safety.")
        print("Disconnect MQTT..")
        mqtt_ads.disconnect_mqtt()
        print("MQTT was disconnected")
        print('Disconnect ADS..')
        print('Release handles')
        for i in pub:
            mqtt_ads.ads.plc.release_handle(pub[i]['handle'])
        for i in sub:
            mqtt_ads.ads.plc.release_handle(sub[i]['handle'])
        mqtt_ads.disconnect_ads()
        print('ADS was disconnected')