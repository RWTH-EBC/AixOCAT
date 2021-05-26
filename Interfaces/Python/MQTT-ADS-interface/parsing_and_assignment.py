# -*- coding: utf-8 -*-
"""
Created on Sat Mar  6 12:01:44 2021

@author: Markus
"""

import re
import pyads
import json

#%%
def getADSVariables(file="TwinCAT Project1/TwinCAT Project1/Untitled1/GVLs/sampleADSGVL.TcGVL"):
    startphrase = 'VAR_GLOBAL'
    endphrase   = 'END_VAR'
    start   = 0
    finish  = 0
    publish = {}
    subscribe = {}
    with open(file) as f:
        for num, line in enumerate(f, 1):
            if startphrase in line:
                start = num + 1
            if endphrase in line:
                finish = num - 1
            if start > 0 and num >= start:
                if finish > 0 and num > finish:
                    break
                s = line.split()
                if s: # skip empty lines
                    # skip all comment lines
                    if re.compile("//").findall(s[0]):
                        continue
                else:
                    continue
                if "%I*" in s[1] or "%I*" in s[2] or "%I*" in s[3]:
                    if re.compile("REAL").findall(s[3]) or re.compile("REAL").findall(s[4]):
                        temptype = pyads.PLCTYPE_REAL
                    elif re.compile("BOOL").findall(s[3]) or re.compile("BOOL").findall(s[4]):
                        temptype = pyads.PLCTYPE_BOOL
                    else:
                        temptype = pyads.PLCTYPE_INT
                    subscribe[s[0]] = temptype
                else:
                    if re.compile("REAL").findall(s[3]) or re.compile("REAL").findall(s[4]):
                        temptype = pyads.PLCTYPE_REAL
                    elif re.compile("BOOL").findall(s[3]) or re.compile("BOOL").findall(s[4]):
                        temptype = pyads.PLCTYPE_BOOL
                    else:
                        temptype = pyads.PLCTYPE_INT
                    publish[s[0]] = temptype
                # print(s)
    # Add control / set point variables to be monitored as well
    # publish.update(subscribe)     # REMARK: It is not possible to read input-only variables (%I*) or internal variables without address/IO assignment via ADS
    print('Done gathering data points. \n')
    return publish, subscribe

def getADSvarsFromSymbols(ads):
    publish     = {}
    subscribe   = {}
    try:
        ads_list    = ads.plc.get_all_symbols()
        for i in ads_list:
            if i.symbol_type == 'BOOL':
                temptype = pyads.PLCTYPE_BOOL
            elif i.symbol_type == 'REAL':
                temptype = pyads.PLCTYPE_REAL
            else:
                temptype = pyads.PLCTYPE_INT
            if "OutData" in i.name:
                #skip
                continue
            if i.index_group == 61472:
                subscribe[i.name] = {'type': temptype, 'handle': ads.plc.get_handle(i.name)}
            elif i.index_group == 61488:
                publish[i.name] = {'type': temptype, 'handle': ads.plc.get_handle(i.name)}
        print('Done gathering data points. \n')
        return publish, subscribe
    except:
        print('There seems to be an issue with the ads connection. Could not fetch data points from plc device via ads.')
        return publish, subscribe

def getRawADSVarListFromSymbols(ads):
    var_list    = {}
    try:
        ads_list    = ads.plc.get_all_symbols()
        for i in ads_list:
            var_list[i.name] = (i.symbol_type, i.index_group)
        return var_list
    except:
        print('There seems to be an issue with the ads connection. Could not fetch data points from plc device via ads.')
        return var_list

#%%
def add_topics(d_all, list_of_datapoints_to_add_topic, topic):
    for i in list_of_datapoints_to_add_topic:
        try:
            d_all[i]['topic'] = topic
        except:
            print('Could not find data point '+i+' in data points, continuing with next entry.')
    return d_all

#%%
def parseJSONpublish(name, value, timestamp):
    res = json.dumps({'name' : name, 'value' : value, 'timestamp' : timestamp}, separators=(',', ':'))
    return res

def parseInfluxDBLinepublish(name, value, timestamp):
    res = f"{name} value={value} {timestamp}"
    return res

def parseJSONsubscribe(payload):
    temp = json.loads(payload)
    return temp['name'], temp['value']

if __name__ == "__main__":
    pub, sub = getADSVariables()