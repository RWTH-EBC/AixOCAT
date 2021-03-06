# -*- coding: utf-8 -*-
"""
Created on Sat Mar  6 12:01:44 2021

@author: Markus
"""

import re

#%%
def getADSVariables():
    startphrase = 'VAR_GLOBAL'
    endphrase   = 'END_VAR'
    start   = 0
    finish  = 0
    with open("sampleADSGVL.TcGVL") as f:
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
                print(s)

    return start, finish, s

if __name__ == "__main__":
    getADSVariables()