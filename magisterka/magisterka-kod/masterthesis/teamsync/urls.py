# -*- coding: utf-8 -*-

from django.conf.urls import url

from . import views
from teamsync.logic import folders, btsync, files

urlpatterns = [
    url(r'^$', views.mainsite, name='mainsite'),
    url(r'^addfolder$', folders.addFolder, name='addFolder'),
    url(r'^deletefolder$', folders.deleteFolder, name='deleteFolder'),
    url(r'^editfolder$', folders.editFolder, name='editFolder'),
    url(r'^getfolders$', folders.getfolders, name='getfolders'),
    url(r'^getitems', folders.getitems, name='getitems'),
    url(r'^editcomment$', files.editcomment, name='editcomment'),
    url(r'^getallcomments$', files.getallcomments, name='getallcomments'),
    url(r'^getcomments$', files.getcomments, name='getcomments'),
    url(r'^getcommentsfrompath$', files.getcommentsfrompath, name='getcommentsfrompath'),
    url(r'^getdominatedthreads$', files.getdominatedthreads, name='getdominatedthreads'),
    url(r'^getconfig$', files.getconfig, name='getconfig'),
    url(r'^getfiles$', files.getfiles, name='getfiles'),
    url(r'^writecomment$', files.writecomment, name='writecomment'),
    url(r'^writenewthread$', files.writenewthread, name='writenewthread'),
    url(r'^btsync/start$', btsync.startbtsync, name='startbtsync'),
    url(r'^btsync/stop$', btsync.stopbtsync, name='stopbtsync'),
]