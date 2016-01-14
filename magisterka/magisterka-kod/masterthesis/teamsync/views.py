#-*- coding: utf-8 -*-

from django.shortcuts import render
from logic import btsync


def mainsite(request):
    if not btsync.isbtsyncactive():
        return render(request, 'teamsync/welcomesite.html')

    return render(request, 'teamsync/mainsite.html')
