#-*- coding: utf-8 -*-

from django.http import HttpResponse, HttpResponseRedirect, Http404, JsonResponse, HttpResponseServerError
from common import config, saveJSON, loadJSON, fileseparator, checkConnection, pattern
import copy
import os
import json
import ntplib


# Struktura przesyłanego JSON'a:
# [ {   'fullpath': ...,
#       'type': 'folder'/'file'/'thread'/'threadfile',
#       'name': <filename>
#   }, { ... } ]
def getfiles(request):
    try:
        result = []
        data = json.loads(request.body)

        if len(data['insidepath']) != 0:
            result = getfilelistfromlocation(data['folderpath'], data['insidepath'])
        else:
            result = getallthreads(data['folderpath'])

        return JsonResponse(result, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas pobierania listy plików.')


def getdominatedthreads(request):
    try:
        data = json.loads(request.body)
        # print json.dumps(data, indent=4)

        files = getallthreads(data['path'])
        threshold = float(data['threshold'][2:4]) / 100     # threshold as fraction
        threads = []
        dominatingusersuid = []

        for user in data['users']:
            if user['isDominating']:
                dominatingusersuid.append(user['uid'])

        for fil in files:
            if fil['type'] in ['folder', 'file']:
                continue

            # main loop for increasing counters of users
            comments = os.listdir(fil['fullpath'])
            userscomments = 0
            limit = fil['numberofcomments'] * threshold

            for commentpath in comments:
                if commentpath == 'meta':
                    continue

                comment = loadJSON(os.path.join(fil['fullpath'], commentpath))
                if comment['uid'] in dominatingusersuid:
                    userscomments += 1

            if (data['threshold'][0] == '>' and float(userscomments) > limit) or \
                    (data['threshold'][0] == '<' and float(userscomments) < limit):
                threads.append(fil)

        return JsonResponse(threads, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas filtrowania wątków.')


def getconfig(request):
    return JsonResponse({
        'threadsorting1': config['threadsorting1'],
        'threadsorting2': config['threadsorting2'],
        'uid': config['uid'],
        'defaultpath': os.path.expanduser('~')
    })


def getcomments(request):
    result = []

    try:
        data = json.loads(request.body)
        fullthreadpath = data['fullthreadpath']
        sortinguid = data['sortinguid']

        for commentfile in os.listdir(fullthreadpath):
            if commentfile == 'meta':
                continue

            comment = loadJSON(os.path.join(fullthreadpath, commentfile))

            if config['uid'] not in comment['readby'].keys():
                comment['readby'][config['uid']] = gettimestamp()

            saveJSON(os.path.join(fullthreadpath, commentfile), comment)

            comment['editing'] = False                                  # need for UI purposes
            comment['historing'] = False                                # need for UI purposes

            result.append(comment)

        if sortinguid != '':
            for res in result:
                if sortinguid not in res['readby'].keys():
                    result.remove(res)
                else:
                    res['timestamp'] = res['readby'][sortinguid]

        result = sorted(result, key=lambda comm: comm['timestamp'])

        return JsonResponse({'comments': result, 'stats': getstats(result)}, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas pobierania komentarzy.')


def getallcomments(request):
    result = []

    try:
        path = json.loads(request.body)['path'] + '/.Comments'

        for tmpdir, subdirs, subfiles in os.walk(path):
            for subdir in subdirs:
                if not pattern.match(subdir):
                    continue

                meta = loadJSON(os.path.join(tmpdir, subdir, 'meta'))
                threaddata = {
                    'fullpath': os.path.join(tmpdir, subdir),
                    'timestamp': meta['timestamp'],
                    'name': meta['topic'],
                    'type': 'thread',
                    'path': os.path.join(tmpdir, subdir).replace(path, ''),
                    'numberofcomments': len(os.listdir(os.path.join(tmpdir, subdir))) - 1,
                    'unreadcomment': False,
                }

                for commentfile in os.listdir(os.path.join(tmpdir, subdir)):
                    if commentfile == 'meta':
                        continue

                    comment = loadJSON(os.path.join(tmpdir, subdir, commentfile))
                    comment['topic'] = threaddata

                    result.append(comment)

        return JsonResponse({'comments': result, 'stats': getstats(result)}, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas pobierania komentarzy.')


def getcommentsfrompath(request):
    result = []

    try:
        data = json.loads(request.body)

        fullpath = os.path.join(data['folderpath'], '.Comments', data['insidepath'][1:])

        if not os.path.isdir(fullpath):
            return JsonResponse([], safe=False)

        for commentfolder in os.listdir(fullpath):
            if not pattern.match(commentfolder):
                continue

            temppath = os.path.join(fullpath, commentfolder)
            meta = loadJSON(os.path.join(temppath, 'meta'))
            threaddata = {
                'fullpath': temppath,
                'timestamp': meta['timestamp'],
                'name': meta['topic'],
                'type': 'thread',
                'path': temppath.replace(data['folderpath'], ''),
                'numberofcomments': len(os.listdir(temppath)) - 1,
                'unreadcomment': False,
            }

            for commentfile in os.listdir(temppath):
                if commentfile == 'meta':
                    continue

                comment = loadJSON(os.path.join(temppath, commentfile))
                comment['topic'] = threaddata

                result.append(comment)

        return JsonResponse({'comments': result}, safe=False)
    except Exception:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas pobierania komentarzy.')


def writecomment(request):
    try:
        if not checkConnection():
            return HttpResponseServerError('Brak połączenia z Internetem.')

        data = json.loads(request.body)

        if len(data['comment']) == 0:
            return HttpResponseServerError('Treść komentarza nie może być pusta.')

        newcomment = {
            'uid': config['uid'],
            'timestamp': gettimestamp(),
            'readby': {},
            'comment': data['comment'],
            'history': []
        }
        newcomment['readby'][config['uid']] = newcomment['timestamp']
        newcomment['history'].append({'timestamp': newcomment['timestamp'], 'comment': newcomment['comment']})

        fullpath = os.path.join(data['fullthreadpath'], newcomment['timestamp'] + fileseparator + config['uid'])

        saveJSON(fullpath, newcomment)

        return JsonResponse(newcomment, safe=False)
    except:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas dodawania komentarza.')


def editcomment(request):
    try:
        if not checkConnection():
            return HttpResponseServerError('Brak połączenia z Internetem.')

        editedcomment = json.loads(request.body)['comment']
        threadfullpath = json.loads(request.body)['threadfullpath']
        #print json.dumps(editedcomment, indent=4)
        #print threadfullpath

        commentpath = os.path.join(threadfullpath, editedcomment['timestamp'] + fileseparator + editedcomment['uid'])

        comment = loadJSON(commentpath)
        comment['history'].append({'timestamp': gettimestamp(), 'comment': editedcomment['comment']})
        comment['comment'] = editedcomment['comment']

        saveJSON(commentpath, comment)

        return JsonResponse({}, safe=False)
    except:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas edycji komentarza.')


def writenewthread(request):
    try:
        if not checkConnection():
            return HttpResponseServerError('Brak połączenia z Internetem.')

        # getting and preparing data
        timestamp = gettimestamp()
        data = json.loads(request.body)

        folderpath = data['folderpath']
        fileabout = '' if data['fileabout'] == "<brak>" else os.path.join(data['insidepath'], data['fileabout'])
        fullthreadpath = os.path.join(folderpath, '.Comments', data['insidepath'][1:], timestamp + fileseparator + config['uid'])

        response = {
            'timestamp': timestamp,
            'name': data['topic'],
            'type': 'thread',
            'numberofcomments': 1,
            'unreadcomment': False,
            'lastcomment': timestamp,
            'fullpath': fullthreadpath,
            'insidepath': fullthreadpath.replace(folderpath + '/.Comments', ''),
        }
        meta = {
            'uid': config['uid'],
            'timestamp': timestamp,
            'topic': data['topic'],
            'fileabout': fileabout
        }
        comment = {
            'uid': config['uid'],
            'timestamp': timestamp,
            'comment': data['comment'],
            'readby': {},
            'history': []
        }
        comment['readby'][config['uid']] = timestamp
        comment['history'].append({'timestamp': timestamp, 'comment': comment['comment']})

        if os.path.isdir(fullthreadpath):
            return HttpResponseServerError('Podany wątek już istnieje.')

        os.makedirs(fullthreadpath)
        saveJSON(os.path.join(fullthreadpath, 'meta'), meta)
        saveJSON(os.path.join(fullthreadpath, timestamp + fileseparator + config['uid']), comment)

        return JsonResponse(response, safe=False)
    except:
        return HttpResponseServerError('Wystąpił nieznany błąd podczas dodawania nowego wątku.')


def getfilelistfromlocation(folderpath, location):
    result = []

    location = location[1:]
    locationfullpath = location == '/' and folderpath or os.path.join(folderpath, location)

    # getting sorted folders only
    for temp in [f for f in sorted(os.listdir(locationfullpath)) if os.path.isdir(os.path.join(locationfullpath, f)) and '.' not in f]:
        fullpath = fullpath = os.path.join(folderpath, location, temp)
        result.append({
            'fullpath': fullpath,
            'name': temp,
            'type': 'folder',
            'insidepath': fullpath.replace(folderpath, '')
        })

    #getting sorted files only
    for temp in [f for f in sorted(os.listdir(locationfullpath)) if os.path.isfile(os.path.join(locationfullpath, f))]:
        fullpath = fullpath = os.path.join(folderpath, location, temp)
        result.append({
            'fullpath': fullpath,
            'name': temp,
            'type': 'file',
            'unrolled': False,
            'threads': [],
            'insidepath': fullpath.replace(folderpath, '')
        })

    # if location contains no comments, then return result
    if not os.path.exists(os.path.join(folderpath, '.Comments', location)) or not os.path.isdir(
            os.path.join(folderpath, '.Comments', location)):
        return result

    # getting threads only
    for temp in os.listdir(os.path.join(folderpath, '.Comments', location)):
        fullpath = os.path.join(folderpath, '.Comments', location, temp)

        if not pattern.match(temp) or not os.path.isdir(fullpath):
            continue

        # getting metadata and lastcomment for it's timestamp
        metadata = loadJSON(os.path.join(fullpath, 'meta'))
        comments = sorted(os.listdir(fullpath), reverse=True)
        lastcomment = loadJSON(os.path.join(fullpath, comments[1]))

        # main thread data
        data = {
            'timestamp': metadata['timestamp'],
            'name': metadata['topic'],
            'type': 'thread',
            'numberofcomments': len(os.listdir(fullpath)) - 1,
            'unreadcomment': False,
            'lastcomment': lastcomment['timestamp'],
            'fullpath': fullpath,
            'insidepath': fullpath.replace(folderpath, '')
        }

        # searching for unread comments
        for comment in comments[1:]:
            comm = loadJSON(os.path.join(fullpath, comment))
            if config['uid'] not in comm['readby'].keys():
                data['unreadcomment'] = True
                break

        # updating files which are thread about
        if len(metadata['fileabout']) > 0:
            for res in result:
                if res['type'] != 'file':
                    continue

                if res['insidepath'] == metadata['fileabout']:
                    if data['unreadcomment']:
                        res['unreadcomment'] = True
                    res['threads'].append(copy.deepcopy(data))
        else:
            result.append(data)

    return result


def getallthreads(folderpath):
    result = []

    for tmpdir, subdirs, subfiles in os.walk(folderpath + '/.Comments'):
        if '.' in tmpdir and '.Comments' not in tmpdir:
            continue

        slashornot = tmpdir == folderpath + '/.Comments' and '/' or ''

        for subdir in subdirs:
            if '.' not in subdir and pattern.match(subdir):
                if not len(os.listdir(os.path.join(tmpdir, subdir))) > 0:
                    continue

                # getting timestamp info about freshest comment
                comments = sorted(os.listdir(os.path.join(tmpdir, subdir)), reverse=True)
                lastcomment = loadJSON(os.path.join(tmpdir, subdir, comments[1]))

                # if comments doesn't contain meta file - ignore thread
                if 'meta' not in comments:
                    continue

                metadata = loadJSON(os.path.join(tmpdir, subdir, 'meta'))
                data = {
                    'fullpath': os.path.join(tmpdir, subdir),
                    'timestamp': metadata['timestamp'],
                    'name': metadata['topic'],
                    'type': 'thread',
                    'path': tmpdir.replace(folderpath + '/.Comments', slashornot),
                    'numberofcomments': len(os.listdir(os.path.join(tmpdir, subdir))) - 1,
                    'unreadcomment': False,
                    'lastcomment': lastcomment['timestamp']
                }

                # searching for unread comments
                for comment in comments:
                    if comment == 'meta':
                        continue
                    comm = loadJSON(os.path.join(tmpdir, subdir, comment))
                    if config['uid'] not in comm['readby'].keys():
                        data['unreadcomment'] = True
                        break

                result.append(data)

    return result


def gettimestamp():
    ntpclient = ntplib.NTPClient()
    response = ntpclient.request(config['ntp_server_name'], version=config['ntp_server_version'])
    return str(int(response.tx_time)) + '000'


def getstats(data):
    stats = {}
    percentages = {}
    for dat in data:
        if dat['uid'] in stats.keys():
            stats[dat['uid']] += 1
        else:
            stats[dat['uid']] = 1

    for key, value in stats.items():
        percentages[key] = str(round(100.0 * float(value) / len(data), 1)).replace('.', ',')

    return {'allcomments': len(data),
            'stats': sorted(stats.items(), key=lambda x: x[1], reverse=True),
            'percentages': percentages}
