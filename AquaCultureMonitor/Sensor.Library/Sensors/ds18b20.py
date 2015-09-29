#!/usr/bin/python

import sys
import os
import glob
import time

import ConfigParser
import requests
import json
import ast

config = ConfigParser.RawConfigParser()
config.read('../appconfig.ini')

sensor = config.get('sensor', 'water')
baseUri = config.get('url','api')

if sensor:

    os.system('modprobe w1-gpio')
    os.system('modprobe w1-therm')

    base_dir = '/sys/bus/w1/devices/'
    device_folders = glob.glob(base_dir + '28*')
    device_files = []
    i = 0
    for folder in device_folders:
        device_files.append(device_folders[i] + '/w1_slave')
        i += 1

    def read_temp_raw(file):
        f = open(file, 'r')
        lines = f.readlines()
        f.close()
        return lines

    def ds18b20_read():
        temps = {}
        for file in device_files:
            lines = read_temp_raw(file)
            while lines[0].strip()[-3:] != 'YES':
                time.sleep(0.2)
                lines = read_temp_raw(file)
            equals_pos = lines[1].find('t=')
            if equals_pos != -1:
                temp_string = lines[1][equals_pos + 2:]
                temp_c = float(temp_string) / 1000.0
                temp_f = temp_c * 9.0 / 5.0 + 32.0

                # Round all measurements.
                temp_c = round(temp_c, 2)
                temp_f = round(temp_f, 2)

                # Get the unique name of the device.
                #device = file
                #device = re.sub('/sys/bus/w1/devices/', '', device)
                #device = re.sub('/w1_slave', '', device)

                # Add the measurement to the temps array.
                #temps[device] = {'C': temp_c, 'F': temp_f}

                url = "{0}/sensor/{1}/reading".format(baseUri, sensor)
                headers = {'Content-Type': 'application/json'}
                payload = {'SensorId': '{0}'.format(sensor), 'MetaValue': '{0}'.format(temp_c)}
                r = requests.post(url, data=json.dumps(payload), headers=headers)
            print 'Water Temp={0:0.1f}*C'.format(temp_c)