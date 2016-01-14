# -*- coding: utf-8 -*-

from django.http import HttpResponse, HttpResponseRedirect, JsonResponse
from os import path
from common import config, createemptybtsyncconfigfile, saveJSON, loadJSON, getUID
import requests
import platform
import subprocess
import psutil
import json
import os
import signal


pid = 0


def isbtsyncactive():
    for proc in psutil.process_iter():
        if proc.name() == 'btsync':
            return True

    return False


def startbtsync(request):
    if isbtsyncactive():
        return HttpResponseRedirect('/')

    # If wrong config file structure raise an exception
    if 'btsync_conf_file' not in config.keys():
        return HttpResponse('Klucz btsync_conf_file nie istnieje w pliku konfiguracyjnym')

    # If btsync-folder doesn't exist, create it
    btsyncconf = loadJSON(config['btsync_conf_file'])
    if not os.path.exists(btsyncconf['storage_path']):
        os.makedirs(btsyncconf['storage_path'])

    # If BTSync config file doesn't exist, create a new one
    if not path.isfile(config['btsync_conf_file']):
        createemptybtsyncconfigfile(config)

    # Start BTSync process
    if platform.system() == 'Windows':
        pass
    elif platform.system() == 'Linux':
        pid = subprocess.Popen([config['btsync_exe_file'], '--config', config['btsync_conf_file']])
        while not isbtsyncactive():     pass
        print 'BTSync started PID = ' + str(pid)

        if 'uid' not in config.keys():
            print 'taking UID'
            config['uid'] = getUID(config['btsync_server_address'])
            print config['uid']
            saveJSON(os.path.join(config['application_path'], 'config.json'), config)

    return HttpResponseRedirect('/')


def stopbtsync(request):
    if not isbtsyncactive():
        return HttpResponseRedirect('/')

    os.kill(pid.pid, signal.SIGKILL)

    return HttpResponseRedirect('/')


def addnewfolder(folderpath, identity, secret='', option=''):
    # CHECKING IF DIR EXISTS
    if not os.path.isdir(folderpath):
        pass

    # GETTING SECRET
    if secret == '':
        js = json.loads(requests.get("http://" + config['btsync_server_address'] + "/api", params={
            'method': 'get_secrets'
        }, auth=('team', 'sync')).text)
        secret = js[option]
        # print "option: " + option + ",secret " + js[option]
        if 'read_write' not in js.keys():
            print "Blad podczas uzyskiwania secreta"
            return

    # ADDING FOLDER
    js2 = json.loads(requests.get("http://" + config['btsync_server_address'] + "/api", params={
        'method': 'add_folder',
        'dir': folderpath,
        'secret': secret
    }, auth=('team', 'sync')).text)

    if js2['error'] == 0:
        print "Dodano folder " + folderpath + " o secrecie " + secret
    else:
        print "Blad podczas dodawania folderu."
        # dolozyc obsluge bledu (najczesciej nie mozna dodac bo folder nie jest pusty)

    # UPDATING CONFIG AND ADDING USER TO .USERS FOLDER
    config['identities'][secret] = identity
    saveJSON(config['application_path'] + '/config/config.json', config)
    # addUser(config, path, identity)
