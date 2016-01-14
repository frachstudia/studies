#-*- coding: utf-8 -*-

from django.shortcuts import render
from django.http import HttpResponse, JsonResponse, HttpResponseServerError
from common import config, saveJSON, loadJSON
import requests
import os
import json


def getfolders(request):
    try:
        data = requests.get("http://" + config['btsync_server_address'] + "/api", params={
            'method': 'get_folders'
        }, auth=('team', 'sync')).json()
        #print json.dumps(data, indent=4)

        for dat in data:
            dat['name'] = dat['dir'].split('/')[-1]
            #print json.dumps(dat, indent=4)

            if os.path.isdir(dat['dir'] + '/.Users'):
                dat['uid'] = config['uid']
                dat['identity'] = loadJSON(dat['dir'] + '/.Users/' + config['uid'] + '.json')['identity']

                files = os.listdir(dat['dir'] + '/.Users')
                users = []

                for fil in files:
                    user = loadJSON(dat['dir'] + '/.Users/' + fil)
                    if user['identity'] == dat['identity']:
                        user['identity'] += ' (Ty)'
                    users.append(user)

                dat['users'] = users

        #print json.dumps(data, indent=4)

        return JsonResponse(data, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił błąd podczas pobierania listy folderów.')


def getitems(request):
    try:
        # receiving path
        result = {}
        data = json.loads(request.body)
        #print json.dumps(data, indent=4)

        if data['move'] == 'back':
            if data['path'].count('/') < 2:
                path = '/'
            else:
                path = "/".join(data['path'].split('/')[:-1])
        elif data['move'] == 'home':
            path = os.path.expanduser('~')
        elif data['move'] == 'next':
            path = os.path.join(data['path'], data['folder'])
        else:
            raise Exception

        listdir = [f for f in os.listdir(path) if not f.startswith('.')]
        result['data'] = [f for f in sorted(listdir, key=lambda s: s.lower()) if os.path.isdir(os.path.join(path, f))]
        result['path'] = path
        result['numberoffiles'] = len(listdir) - len([f for f in listdir if os.path.isdir(os.path.join(path, f))])

        #print json.dumps(result, indent=4)
        return JsonResponse(result, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił błąd podczas pobierania listy folderów.')


def addFolder(request):
    try:
        # receiving keys: path, secret, identity
        data = json.loads(request.body)

        # CHECKING IF USER PASSED NO IDENTITY
        if data['identity'] == '':
            return HttpResponseServerError('Pole tożsamości jest puste.')

        # CHECKING IF USER PASSED TOO LONG IDENTITY
        if len(data['identity']) > 25:
            return HttpResponseServerError('Zbyt długa tożsamość.')

        # CHECKING IF DIR EXISTS
        if not os.path.isdir(data['path']):
            return HttpResponseServerError('Wybrany folder nie istnieje.')

        # CHECKING IF DIR IS EMPTY
        if os.listdir(data['path']) != []:
            return HttpResponseServerError('Wybrany folder nie jest pusty.')

        #CHECKING SECRET
        if len(data['secret']) != 33 and len(data['secret']) != 0:
            return HttpResponseServerError('Wpisany secret ma złą długość.')

        # GETTING SECRET
        if data['secret'] == '':
            js = json.loads(requests.get("http://" + config['btsync_server_address'] + "/api", params={
                'method': 'get_secrets'
            }, auth=('team', 'sync')).text)

            if 'read_write' not in js.keys():
                return HttpResponseServerError('Wystąpił błąd podczas uzyskiwania secreta.')

            data['secret'] = js['read_write']

        # ADDING FOLDER
        js2 = json.loads(requests.get("http://" + config['btsync_server_address'] + "/api", params={
            'method': 'add_folder',
            'dir': data['path'],
            'secret': data['secret']
        }, auth=('team', 'sync')).text)

        if js2['error'] != 0:
            return HttpResponseServerError('Wystąpił błąd podczas dodawania folderu.')

        # UPDATING CONFIG
        config['identities'][data['secret']] = data['identity']
        saveJSON(config['application_path'] + '/config.json', config)

        # CREATING .COMMENTS DIRECTORY
        if not os.path.isdir(data['path'] + '/.Comments'):
            os.makedirs(data['path'] + '/.Comments')

        # CREATING .USERS DIRECTORY
        if not os.path.isdir(data['path'] + '/.Users'):
            os.makedirs(data['path'] + '/.Users')

        # ADDING USER TO .USERS DIRECTORY
        saveJSON(data['path'] + '/.Users/' + config['uid'] + '.json', {
            'uid': config['uid'],
            'identity': data['identity']
            # todo another config goes here for example color of comments
        })

        return HttpResponse('Dodano folder.')
    except (Exception):
        return HttpResponseServerError('Wystąpił nieznany błąd podczas dodawania folderu.')


def editFolder(request):
    try:
        data = json.loads(request.body)

        #print data
    except (Exception):
        return HttpResponseServerError('Wystąpił nieznany błąd podczas edycji folderu.')


def deleteFolder(request):
    try:
        data = json.loads(request.body)

        js = json.loads(requests.get("http://" + config['btsync_server_address'] + "/api", params={
            'method': 'remove_folder',
            'secret': data['secret']
        }, auth=('team', 'sync')).text)

        if js['error'] == 0:
            # POPPING IDENTITY FROM LIST OF OUR IDENTITIES IN CONFIG FILE
            if data['secret'] in config['identities'].keys():
                config['identities'].pop(data['secret'])

            # SAVING CONFIG FILE
            saveJSON(config['application_path'] + '/config.json', config)

            return HttpResponse('Usunięto folder.')
        else:
            return HttpResponseServerError('Wystąpił błąd podczas usuwania folderu.')
    except (Exception):
        return HttpResponseServerError('Wystąpił nieznany błąd podczas usuwania folderu.')