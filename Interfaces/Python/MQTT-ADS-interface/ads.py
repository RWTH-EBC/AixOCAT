# -*- coding: utf-8 -*-
"""
Created on Wed Feb 24 10:43:24 2021

@author: Markus
"""

import pyads

#%%
class ads():
    
    def __init__(self, port=851):
        self.port = port
    
    def create_route(self, ams_netID="5.53.34.234.1.1", remote_ip="134.130.56.144"):
        pyads.ads.add_route_to_plc(ams_net_id=ams_netID, 
                                   local_ip="134.130.56.149", 
                                   remote_ip=remote_ip,
                                   user="Administrator",
                                   password="1",
                                   route_name="Route_to_PLC_"+remote_ip,
                                   added_net_id="134.130.56.149.1.1")

        adr = pyads.AmsAddr(ams_netID, pyads.PORT_SPS1)
        pyads.add_route(adr, remote_ip)
        
    def connect(self, ams_netID="5.53.34.234.1.1", host="134.130.56.144"):
        # Open the ADS connection
        self.plc = pyads.Connection(
            ams_net_id= ams_netID,
            ams_net_port=self.port, 
            ip_address=host)   
        self.plc.open()
    
    def disconnect(self):
        self.plc.close()
    
    def read(self, var):
        res = self.plc.read_by_name(var)
        return res
    
#%%