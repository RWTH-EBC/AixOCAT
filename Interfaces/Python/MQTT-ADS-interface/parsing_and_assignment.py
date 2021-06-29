# -*- coding: utf-8 -*-
"""
Created on Sat Mar  6 12:01:44 2021

@author: Markus
"""

import re
import pyads
import json

#%%
def getADSVariables(file="TwinCAT Project_MQTT_Example/TwinCAT Project1/MQTT_Example/GVLs/sampleADSGVL.TcGVL"):
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

def getMarkedADSVariables(file="TwinCAT Project_MQTT_Example/TwinCAT Project1/MQTT_Example/GVLs/sampleADSGVL_markedVariables.TcGVL"):
    startphrase = 'VAR_GLOBAL'
    endphrase   = 'END_VAR'
    start   = 0
    finish  = 0
    publish = {}
    subscribe = {}

    # To mark a variable put the comment //# in the respective line of the *TcGVL file
    # Put further comments after the mark with at least one space, like this: //W -> //# W

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

                # the mark is always at position 5 in s, counting from 0
                if (len(s) >= 6) and (s[5] == "//#"):
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

    print('Done gathering data points. \n')
    return publish, subscribe

def getADSvarsFromSymbols(ads):
    publish     = {}
    subscribe   = {}
    try:
        ads_list    = ads.plc.get_all_symbols()
        #ads_list    = ads.get_all_symbols()
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

def getMarkedADSvarsFromSymbols(plc):
    publish = {}
    subscribe = {}

    # Desired variables have to be marked with a single //# comment
    try:
        symbol_list = plc.get_all_symbols()
        for symbol in symbol_list:
            if symbol.comment == '#':
                if symbol.symbol_type == 'BOOL':
                    temptype = pyads.PLCTYPE_BOOL
                elif symbol.symbol_type == 'REAL':
                    temptype = pyads.PLCTYPE_REAL
                else:
                    temptype = pyads.PLCTYPE_INT
                if "OutData" in symbol.name:
                    #skip
                    continue
                if symbol.index_group == 61472:
                    subscribe[symbol.name] = {'type': temptype, 'handle': plc.get_handle(symbol.name)}
                if symbol.index_group == 61488:
                    publish[symbol.name] = {'type': temptype, 'handle': plc.get_handle(symbol.name)}
        print('Done gathering data points. \n')
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
