#-*- coding: utf-8 -*-

from django.conf.urls import url
from . import views
from patients.urlpatterns import format_suffix_patterns


urlpatterns = [
    url(r'^$', views.login),
    url(r'^patients/categorys$', views.CategoryList.as_view()),
    url(r'^patients/categorys/(?P<pk>[0-9]+)$', views.CategoryDetail.as_view()),
    url(r'^patients/doctors$', views.DoctorList.as_view()),
    url(r'^patients/doctors/(?P<pk>[0-9]+)$', views.DoctorDetail.as_view()),
    url(r'^patients/logout$', views.logout),
    url(r'^patients/main$', views.mainSite),
    url(r'^patients/periodictests$', views.PeriodicTestList.as_view()),
    url(r'^patients/periodictests/(?P<pk>[0-9]+)$', views.PeriodicTestDetail.as_view()),
    url(r'^patients/places$', views.PlaceList.as_view()),
    url(r'^patients/places/(?P<pk>[0-9]+)$', views.PlaceDetail.as_view()),
    url(r'^patients/profiles$', views.ProfileList.as_view()),
    url(r'^patients/profiles/(?P<pk>[0-9]+)$', views.ProfileDetail.as_view()),
    url(r'^patients/profileresults', views.ProfileResultsList.as_view()),
    url(r'^patients/profileresults/(?P<pk>[0-9]+)$', views.ProfileResultsDetail.as_view()),
    url(r'^patients/register$', views.register),
    #url(r'^patients/report$', views.createReport()),
    url(r'^patients/results$', views.ResultList.as_view()),
    url(r'^patients/results/(?P<pk>[0-9]+)$', views.ResultDetail.as_view()),
    url(r'^patients/postprofileresults$', views.postProfileResults),
    url(r'^patients/show$', views.show),
    url(r'^patients/testcategorys$', views.TestCategoryList.as_view()),
    url(r'^patients/testcategorys/(?P<pk>[0-9]+)$', views.TestCategoryDetail.as_view()),
    url(r'^patients/testprofiles$', views.TestProfileList.as_view()),
    url(r'^patients/testprofiles/(?P<pk>[0-9]+)$', views.TestProfileDetail.as_view()),
    url(r'^patients/tests$', views.TestList.as_view()),
    url(r'^patients/tests/(?P<pk>[0-9]+)$', views.TestDetail.as_view()),
    url(r'^patients/units$', views.UnitList.as_view()),
    url(r'^patients/units/(?P<pk>[0-9]+)$', views.UnitDetail.as_view()),
    url(r'^patients/users$', views.UserList.as_view()),
    url(r'^patients/users/(?P<pk>[0-9]+)$', views.UserDetail.as_view()),
    url(r'^patients/visits$', views.VisitList.as_view()),
    url(r'^patients/visits/(?P<pk>[0-9]+)$', views.VisitDetail.as_view()),
]

urlpatterns = format_suffix_patterns(urlpatterns)