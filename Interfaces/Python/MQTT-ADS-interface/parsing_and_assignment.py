# -*- coding: utf-8 -*-
"""
Created on Sat Mar  6 12:01:44 2021

@author: Markus
"""

import re

#%%
def getADSVariables(file = "sampleADSGVL.TcGVL"):
    startphrase = 'VAR_GLOBAL'
    endphrase   = 'END_VAR'
    start   = 0
    finish  = 0
    publish = []
    subscribe = []
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
                        temptype = "REAL"
                    elif re.compile("BOOL").findall(s[3]) or re.compile("BOOL").findall(s[4]):
                        temptype = "BOOL"
                    else:
                        temptype = "INT"
                    subscribe.append([s[0], temptype])
                else:
                    if re.compile("REAL").findall(s[3]) or re.compile("REAL").findall(s[4]):
                        temptype = "REAL"
                    elif re.compile("BOOL").findall(s[3]) or re.compile("BOOL").findall(s[4]):
                        temptype = "BOOL"
                    else:
                        temptype = "INT"
                    publish.append([s[0], temptype])
                print(s)

    return publish, subscribe

if __name__ == "__main__":
    pub, sub = getADSVariables()