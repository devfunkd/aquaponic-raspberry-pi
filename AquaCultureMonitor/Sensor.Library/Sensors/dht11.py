#!/usr/bin/python
import os
import glob
import time
import subprocess
import sys
import Adafruit_DHT

import ConfigParser
import requests
import json
import ast

config = ConfigParser.RawConfigParser()
config.read('../appconfig.ini')

sensorTemperature = config.get('sensor', 'air')
sensorHumidity = config.get('sensor', 'humidity')
baseUri = config.get('url','api')

def dht11_read():
    humidity, temperature = Adafruit_DHT.read_retry(22, 17)

    if temperature is not None:
        url = "{0}/sensor/{1}/reading".format(baseUri, sensorTemperature)
        headers = {'Content-Type': 'application/json'}
        payload = {'SensorId': '{0}'.format(sensorTemperature), 'MetaValue': '{0}'.format(temperature)}
        r = requests.post(url, data=json.dumps(payload), headers=headers)
        print 'Air Temp={0:0.1f}*C'.format(temperature)

    if humidity is not None:
        url = "{0}/sensor/{1}/reading".format(baseUri, sensorHumidity)
        headers = {'Content-Type': 'application/json'}
        payload = {'SensorId': '{0}'.format(sensorHumidity), 'MetaValue': '{0}'.format(humidity)}
        r = requests.post(url, data=json.dumps(payload), headers=headers)
        print 'Humidity={0:0.1f}%'.format(humidity)