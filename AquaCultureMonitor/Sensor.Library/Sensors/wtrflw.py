#!/usr/bin/python
import os
import glob
import time
import subprocess
import sys
import RPi.GPIO as GPIO
from flowmeter import *

import ConfigParser
import requests
import json
import ast

config = ConfigParser.RawConfigParser()
config.read('../appconfig.ini')

sensorWaterFlow = config.get('sensor', 'flow')
baseUri = config.get('url','api')

#boardRevision = GPIO.RPI_REVISION
GPIO.setmode(GPIO.BCM) # use real GPIO numbering
GPIO.setup(24,GPIO.IN, pull_up_down=GPIO.PUD_UP)

# set up the flow meters
fm = FlowMeter('metric')

def doAClick(channel):
  currentTime = int(time.time() * FlowMeter.MS_IN_A_SECOND)
  if fm.enabled == True:
    fm.update(currentTime)

GPIO.add_event_detect(24, GPIO.RISING, callback=doAClick, bouncetime=20)

# main loop
def get_flow():
    
    time.sleep(1)
    currentTime = int(time.time() * FlowMeter.MS_IN_A_SECOND)
    flow = fm.getFormattedFlow()
    
    if flow is not None:
        url = "{0}/sensor/{1}/reading".format(baseUri, sensorWaterFlow)
        headers = {'Content-Type': 'application/json'}
        payload = {'SensorId': '{0}'.format(sensorWaterFlow), 'MetaValue': '{0}'.format(fm.flow)}
        r = requests.post(url, data=json.dumps(payload), headers=headers)
        print r.json
        print 'WaterFlow={0}'.format(flow)