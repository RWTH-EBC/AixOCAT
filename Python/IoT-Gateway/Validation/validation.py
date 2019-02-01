# -*- coding: utf-8 -*-
"""
Created on Wed Jan  2 15:48:30 2019

@author: msh
"""

from matplotlib import pyplot as plt
import numpy as np
import xlrd

file = 'measurements_all.xlsx'

book = xlrd.open_workbook(file)
s       = {'V_in_ESP32#1': 0, 'V_in_ESP32#2': 1, 'V_in_ESP32#3': 2, 'V_in_ESP32#4': 3, 'V_in_ESP32#5': 4,
          'I_in_ESP32#1': 5, 'I_in_ESP32#2': 6, 'I_in_ESP32#3': 7, 'I_in_ESP32#4': 8, 'I_in_ESP32#5': 9,
          'V_out_ESP32#1': 10, 'V_out_ESP32#2': 11, 'V_out_ESP32#3': 12, 'V_out_ESP32#4': 13, 'V_out_ESP32#5': 14,
          'I_out_ESP32#1': 15, 'I_out_ESP32#2': 16, 'I_out_ESP32#3': 17, 'I_out_ESP32#4': 18, 'I_out_ESP32#5': 19}
sheets = {}
for j in s:
    sheets[j] = book.sheet_by_index(s[j])
V_in = {}
I_in = {}
V_out = {}
I_out = {}

zero = False
if zero == True:
    b = 1
else:
    b = 4
V_in['boards'] = []
for j in sheets:
    if 'V_in' in j:
        V_in['boards'].append(j)
        V_in[j] = {}
        V_in[j]['predefined'] = np.array([sheets[j].col(0)[x].value for x in range(1, 22)])
        V_in[j]['measured'] = np.array([sheets[j].col(1)[x].value for x in range(1, 22)])
        V_in[j]['together'] = np.column_stack((V_in[j]['predefined'], V_in[j]['measured']))
        V_in[j]['diff_abs'] = np.abs(np.diff(V_in[j]['together'], axis=1))
        V_in[j]['diff_rel'] = np.divide(V_in[j]['diff_abs'][b:,0], V_in[j]['predefined'][b:])*100
V_in['measured_avg'] = np.average(np.column_stack((V_in[j]['measured'] for j in V_in['boards'])), axis=1)
I_in['boards'] = []
for j in sheets:
    if 'I_in' in j:
        I_in['boards'].append(j)
        I_in[j] = {}
        I_in[j]['predefined'] = np.array([sheets[j].col(0)[x].value for x in range(1, 22)])
        I_in[j]['measured'] = np.array([sheets[j].col(1)[x].value for x in range(1, 22)])
        I_in[j]['together'] = np.column_stack((I_in[j]['predefined'], I_in[j]['measured']))
        I_in[j]['diff_abs'] = np.abs(np.diff(I_in[j]['together'], axis=1))
        I_in[j]['diff_rel'] = np.divide(I_in[j]['diff_abs'][b:,0], I_in[j]['predefined'][b:])*100
I_in['measured_avg'] = np.average(np.column_stack((I_in[j]['measured'] for j in I_in['boards'])), axis=1)
V_out['boards'] = []
for j in sheets:
    if 'V_out' in j:
        V_out['boards'].append(j)
        V_out[j] = {}
        V_out[j]['predefined'] = np.array([sheets[j].col(0)[x].value for x in range(1, 22)])
        V_out[j]['measured'] = np.array([sheets[j].col(1)[x].value for x in range(1, 22)])
        V_out[j]['together'] = np.column_stack((V_out[j]['predefined'], V_out[j]['measured']))
        V_out[j]['diff_abs'] = np.abs(np.diff(V_out[j]['together'], axis=1))
        V_out[j]['diff_rel'] = np.divide(V_out[j]['diff_abs'][b:,0], V_out[j]['predefined'][b:])*100
V_out['measured_avg'] = np.average(np.column_stack((V_out[j]['measured'] for j in V_out['boards'])), axis=1)
I_out['boards'] = []
for j in sheets:
    if 'I_out' in j:
        I_out['boards'].append(j)
        I_out[j] = {}
        I_out[j]['predefined'] = np.array([sheets[j].col(0)[x].value for x in range(1, 22)])
        I_out[j]['measured'] = np.array([sheets[j].col(1)[x].value for x in range(1, 22)])
        I_out[j]['together'] = np.column_stack((I_out[j]['predefined'], I_out[j]['measured']))
        I_out[j]['diff_abs'] = np.abs(np.diff(I_out[j]['together'], axis=1))
        I_out[j]['diff_rel'] = np.divide(I_out[j]['diff_abs'][b:,0], I_out[j]['predefined'][b:])*100
I_out['measured_avg'] = np.average(np.column_stack((I_out[j]['measured'] for j in I_out['boards'])), axis=1)
#%% test series - plot measurement data
# voltage
predesc_val = np.array([0.0,0.5,1.0,1.5,2.0,2.5,3.0,3.5,4.0,4.5,5.0,5.5,6.0,6.5,7.0,7.5,8.0,8.5,9.0,9.5,10.0])
volt_in = V_in['measured_avg']     # 0-10 V input
volt_out = V_out['measured_avg']  # 0-10 V output

line1, = plt.plot(predesc_val,
         predesc_val,'k:', label="ref")
line2, = plt.plot(predesc_val,
         volt_in, 'b+', label='V_in')
line3, = plt.plot(predesc_val,
         volt_out, 'r.', label='V_out')
plt.ylabel('Measured value')
plt.xlabel('Predefined value')
ax=plt.gca()
ax.axes.set_xlim(-0.25,10.25)
ax.axes.set_ylim(-0.25,10.25)
ax.axes.set_xticks([0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0])
ax.axes.set_yticks([0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0])
plt.legend()

plt.savefig('validaition_1.pdf', dpi=300)
plt.savefig('validaition_1.png', dpi=300)

plt.show()
plt.close()

# current
curr_in = I_in['measured_avg']   # 0-20 mA input
curr_out = I_out['measured_avg']   # 0-20 mA output

line1, = plt.plot(predesc_val*2,
         predesc_val*2,'k:', label="ref")
line2, = plt.plot(predesc_val*2,
         curr_in, 'b+', label='I_in')
line3, = plt.plot(predesc_val*2,
         curr_out, 'r.', label='I_out')
plt.ylabel('Measured value')
plt.xlabel('Predefined value')
ax=plt.gca()
ax.axes.set_xlim(-0.5,20.5)
ax.axes.set_ylim(-0.5,20.5)
ax.axes.set_xticks([0.0, 2.0, 4.0, 6.0, 8.0, 10.0, 12.0, 14.0, 16.0, 18.0, 20.0])
ax.axes.set_yticks([0.0, 2.0, 4.0, 6.0, 8.0, 10.0, 12.0, 14.0, 16.0, 18.0, 20.0])
plt.legend()

plt.savefig('validaition_2.pdf', dpi=300)
plt.savefig('validaition_2.png', dpi=300)

plt.show()
plt.close()

#%% calculate deviations
if b > 1:
    pass
else:
    b = 0

V_in['avg_rel'] = np.average(np.column_stack((V_in[j]['diff_rel'] for j in V_in['boards'])), axis=1)
V_in['avg_abs'] = np.average(np.column_stack((V_in[j]['diff_abs'] for j in V_in['boards'])), axis=1)
V_in["error_rel"] = np.average(V_in['avg_rel'])
V_in["error_abs"] = np.average(V_in['avg_abs'][b:])
I_in['avg_rel'] = np.average(np.column_stack((I_in[j]['diff_rel'] for j in I_in['boards'])), axis=1)
I_in['avg_abs'] = np.average(np.column_stack((I_in[j]['diff_abs'] for j in I_in['boards'])), axis=1)
I_in["error_rel"] = np.average(I_in['avg_rel'])
I_in["error_abs"] = np.average(I_in['avg_abs'][b:])
V_out['avg_rel'] = np.average(np.column_stack((V_out[j]['diff_rel'] for j in V_out['boards'])), axis=1)
V_out['avg_abs'] = np.average(np.column_stack((V_out[j]['diff_abs'] for j in V_out['boards'])), axis=1)
V_out["error_rel"] = np.average(V_out['avg_rel'])
V_out["error_abs"] = np.average(V_out['avg_abs'][b:])
I_out['avg_rel'] = np.average(np.column_stack((I_out[j]['diff_rel'] for j in I_out['boards'])), axis=1)
I_out['avg_abs'] = np.average(np.column_stack((I_out[j]['diff_abs'] for j in I_out['boards'])), axis=1)
I_out["error_rel"] = np.average(I_out['avg_rel'])
I_out["error_abs"] = np.average(I_out['avg_abs'][b:])

#%% prices

# source:
# resistors:
# 100 pieces
# 10 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-10K?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLEAsgrzGqVg0%3d
# 33 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-33K?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLr%2flpdB0jhEI%3d
# 45.3 https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-45R3?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLfdeez06oetc%3d
# 22 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-22K?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLpNJ%2f2LE5vFQ%3d
# 15 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-15K?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLhMH%252bFV5CcV4%3d
# 680 https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-680R?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLTwhDAx9EVIo%3d
# 1 M https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-1M?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLOnqnO2puWKI%3d
# 2 M https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-2M?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLG7YE3WBvOtk%3d
# 13 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-13K?qs=sGAEpiMZZMu61qfTUdNhG0IXHLFuiNndG9CdV23W2Qg%3d
# 6.8 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-6K8?qs=%2fha2pyFadui%252bnL3Q99GBfi3U15u5a5HFzw5PEwjRzOvKH21M1bZzWg%3d%3d
# 200 pieces
# 3.3 k https://www.mouser.de/ProductDetail/Yageo/MFR-25FBF52-3K3?qs=sGAEpiMZZMsPqMdJzcrNwiweiCzxKzWLOgyXHVGfGpA%3d
# capacitors:
# 100 pieces
# 100 nF https://www.mouser.de/ProductDetail/Vishay-BC-Components/K104K15X7RF5TH5V?qs=sGAEpiMZZMsh%252b1woXyUXjxZ%2f4fVzYdXZhyWgqhwEc3U%3d
# 1 uF https://www.mouser.de/ProductDetail/Vishay-Sprague/173D105X9025UW?qs=sGAEpiMZZMsh%252b1woXyUXj0w7ITK0P1WJImQ9T5qvYvk%3d
# OPAs:
# 100 pieces
# OPA344PA https://www.mouser.de/ProductDetail/Texas-Instruments/OPA344PA?qs=%2fha2pyFaduiQZFi9ot%252bqcJhkUppO2FgLx7q7NJbZG%2fA%3d
# OPA705PA https://www.mouser.de/ProductDetail/Texas-Instruments/OPA705PA?qs=%2fha2pyFaduh6sSiT292YJCDxffAncMOIY5PBKoCJbuE%3d
# Special parts
# 100 pieces
# XTR117 https://www.mouser.de/ProductDetail/Texas-Instruments/XTR117AIDGKR?qs=sGAEpiMZZMsLZKpoLkeUsZ%2f5I3yW2hwg%2f0ri%252bO14pK0%3d
# BC337 (transistor) https://www.mouser.de/ProductDetail/Taiwan-Semiconductor/BC337-40-A1?qs=sGAEpiMZZMshyDBzk1%2fWiwIK8FF8XsC1RNbmjp0kCPo%3d
# XY-485 http://www.prodctodc.com/ttl-to-rs485-module-dc-30v-30v-rs485-to-ttl-mutual-conversion-hardware-automatic-flow-control-moduleconverter-module-p-820.html#.XC3xxsSDOUk
# 2.56 $ => exchange rate (03.01.2019): 0.88 €/$ => 2.2528 €
# LM340T-5.0 https://www.mouser.de/ProductDetail/Texas-Instruments/LM340T-50-NOPB?qs=sGAEpiMZZMtUqDgmOWBjgJwP%252b5MP6LxOZQvT5SiE6M8%3d
# REF102AU https://www.mouser.de/ProductDetail/Texas-Instruments/REF102AU-2K5?qs=sGAEpiMZZMuBck1X%252b7j9fIs%252ba2gLkdiAvLbAxhP%252bKhc%3d
# ESP32-PICO-KIT https://www.mouser.de/ProductDetail/Espressif-Systems/ESP32-PICO-KIT?qs=MLItCLRbWsyoLrlknFRqcQ%3D%3D&gclid=EAIaIQobChMI1tuo6sXR3wIVCud3Ch0vkAgbEAAYASAAEgIJZ_D_BwE
# CNX82A https://www.reichelt.de/optokoppler-cnx-82a-p6673.html
price_0_10_V_in = {"cap_1u":0.036, "opa_344pa":0.891, "cap_100n":0.024,
                   "res_33k":0.003, "res_3_3k":0.003, "res_10k":0.003}
price_0_20_mA_in = {"cap_1u":0.036, "opa_344pa":0.891, "cap_100n":0.024,
                   "res_45_3":0.003, "res_10k":0.003}
price_0_10_V_out = {"cap_1u":0.036, "opa_705pa":0.891, "cap_100n":0.024,
                   "res_22k":0.003, "res_10k_1":0.003, "res_10k_2":0.003}
price_0_20_mA_out = {"xtr117":1.39, "bc337":0.065, "opa_344pa":0.891, "cnx82a":0.10,
                     "res_10k":0.003, "cap_1u":0.036, "res_13k":0.003, "res_6_8k":0.003}
price_modbus = {"xy-k485":1.18, "res_680":0.003}
price_vref = {"res_1m_1":0.003, "res_1m_2":0.003,
              "res_2m_1":0.003, "res_2m_2":0.003}
price_power = {"lm340t-5_0":1.06, "ref102au":3.23, "cap_220n":0.024, "cap100n":0.024}
price_esp32 = {"esp32-pico-kit":8.73}
price_connectors = {"term_block_screw":1.425, "sockets":2.525}
prices = [price_0_10_V_in,price_0_20_mA_in,price_0_10_V_out,price_0_20_mA_out,price_modbus,price_power,price_esp32,price_connectors]#price_vref,
names = {0:"V_in", 1:"V_out", 2:"I_in", 3:"I_out", 4:"rs485", 5:"power", 6:"esp", 7:"conn"}#5:"vref", 

temp = {}
price_modules = {}
for d in prices:
    temp[prices.index(d)] = sum(v for v in d.values())
for k in temp.keys():
    price_modules[names[k]] = temp[k]
del temp
price_total = sum(sum(v for v in dic.values()) for dic in prices)

#%% plot prices
temp = np.array([v for v in price_modules.values()])
def absolute_value(v):
    j = np.round(v/100*temp.sum(), 2)
    return j

plt.pie([v for v in price_modules.values()], labels=[k for k in price_modules.keys()], autopct=absolute_value)
plt.axis("image")

plt.savefig('prices.png', dpi=300)

plt.show()
plt.close()