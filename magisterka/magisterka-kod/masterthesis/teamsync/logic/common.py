#-*- coding: utf-8 -*-

import io
import json
from os import *
import platform
import requests
import subprocess
import re


def loadJSON(filepath):
    with io.open(filepath, 'r', encoding='utf-8') as f:
        result = json.load(f, encoding='utf-8')

    return result


def saveJSON(filepath, data):
    with io.open(filepath, 'w', encoding='utf-8') as f:
        f.write(json.dumps(data, ensure_ascii=False, encoding='utf8', indent=4))


def loadconfigfile():
    # if btsyncconf.json is not present, create it
    if not path.isfile(getrootpath() + '/masterthesis/teamsync/btsyconncfig.json'):
        createemptybtsyncconfigfile()

    if not path.isfile(getrootpath() + '/masterthesis/teamsync/config.json'):
        return createemptyconfigfile()
    else:
        return loadJSON(getrootpath() + '/masterthesis/teamsync/config.json')


def getrootpath():
    return '/'.join(getcwd().split('/')[:-1])


def getUID(btsyncaddress):
    js = json.loads(requests.get("http://" + btsyncaddress + "/api", params={
        'method': 'get_secrets'
    }, auth=('team', 'sync')).text)
    return js['share_id']


def createemptyconfigfile():
    result = {"ntp_server_name": "pl.pool.ntp.org", "ntp_server_version": 3, "first_time": "yes",
            "operating_system": platform.system(), "application_path": getrootpath() + '/masterthesis/teamsync',
            "btsync_conf_file": getrootpath() + "/masterthesis/teamsync/btsyncconfig.json",
            "btsync_server_address": "127.0.0.1:8787", "btsync_exe_file": getrootpath() + "/masterthesis/teamsync/btsync",
            "display_name": "", "comments_date_format": "HH:mm - d.MM.yyyy", "identities": {},
            "threadsorting1": "0", "threadsorting2": "0"}
    saveJSON(getrootpath() + '/masterthesis/teamsync/config.json', result)

    return result


def createemptybtsyncconfigfile():
    result = {
        "storage_path": getrootpath() + "/.btsync-files", "use_gui": False,
        "webui": {
            "listen": '127.0.0.1:8787', "login": "team", "password": "sync",
            "api_key": "FHNMHX7UQECBS2SEKQHVFKAK6UGRKVXARU5GJSMQ3RZGMPLMVRNYR6C273H23NI5PSFPCQX7P74Z5OFKTZEXRC4CQ2NXCY2ZA55ECC5HRI63YMHPRID7BNIHQBBC22RJAYORPEQ"
        }
    }
    saveJSON(getrootpath() + '/masterthesis/teamsync/btsyncconfig.json', result)
    return result


def checkConnection():
    try:
        requests.get("http://google.com")
        return True
    except:
        return False


config = loadconfigfile()
fileseparator = '@#&$'
pattern = re.compile("^[0-9]{13}@#&\$[0-9A-Z]{32}$")
