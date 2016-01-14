#-*- coding: utf-8 -*-

from django import forms
from django.contrib import auth
from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import User
from django.db.models import Q
from django.http import HttpResponse, HttpResponseRedirect, Http404
from django.shortcuts import render
from django.views.decorators.csrf import csrf_exempt
from forms import *
from rest_framework import status
from rest_framework import generics
from rest_framework import permissions
from patients.models import *
from patients.serializers import *
from permissions import IsOwner
import md5
import json

# Create your views here.
def login(request):
    if request.user.is_authenticated():
        return HttpResponseRedirect('/patients/main')

    if request.method == 'POST':
        username = request.POST.get('username','')
        password = request.POST.get('password','')
        user = auth.authenticate(username=username, password=password)

        if user is not None:
            auth.login(request, user)
            return HttpResponseRedirect('/patients/main')
        else:
            return render(request, "patients/login.html", {'err': 'Zły numer PESEL lub hasło.'})
    else:
        return render(request, "patients/login.html")

    return render(request, "patients/login.html")

def logout(request):
    auth.logout(request)
    # todo NIE DZIAŁA WYLOGOWYWANIE
    print 'lalala'

    return HttpResponseRedirect('/')

def register(request):
    if request.method == 'POST':
        form = MyRegistrationForm(request.POST)
        if form.is_valid():
            new_user = form.save()
            return render(request, "patients/login.html", {'justRegistered': 'Rejestracja przebiegła pomyślnie. Możesz się zalogować.'})
    else:
        form = MyRegistrationForm()

    return render(request, "patients/register.html", {'form': form})

def mainSite(request, format=None):
    #hash = md5.new('Dupa').hexdigest()
    #print hash.hexdigest()

    if not request.user.is_authenticated():
        return HttpResponseRedirect('/')

    if request.user.is_superuser:
        return HttpResponseRedirect('/admin')

    sex = (int(request.user.username[9]) % 2 == 0) and 'K' or 'M'

    return render(request, "patients/mainSite.html", {'tests': Test.objects.all().filter(Q(sex=sex) | Q(sex='')).order_by('name'),
                                                      'units': Unit.objects.order_by('unit'),
                                                      'doctors': Doctor.objects.all().order_by('name'),
                                                      'places': Place.objects.all().order_by('name'),
                                                      'results': Result.objects.all().filter(owner=request.user),
                                                      'sex': sex})

def createReport(request):
    pass

def show(request):
    if request.method == 'POST':
        username = request.POST.get('username','')
        password = request.POST.get('password','')
        hash = md5.new(username + password).hexdigest()

        print 'user: ' + username
        print 'password: ' + password
        print 'hash: ' + hash

        profile = Profile.objects.all().filter(hash=hash)

        print profile

        if len(profile) == 0:
            return render(request, 'patients/doctorSiteWrongHash.html', {})

        print 'user: ' + username
        print 'password: ' + password
        print 'hash: ' + hash

        hashresults = ProfileResults.objects.all().filter(profile=profile)

        print len(hashresults)

        return render(request, 'patients/doctorSite.html', {'hashresults': hashresults,
                                                            'sex': (int(username[9]) % 2 == 0) and 'K' or 'M',
                                                            'username': username,})

def postProfileResults(request):
    if request.method == 'POST':
        data = json.loads(request.body)
        profile = Profile.objects.get(id=data['profileID'])

        if data['activate'] == 'yes':
            results = Result.objects.all().filter(owner=request.user,test__id__in=data['tests'])

            for result in results:
                temp = ProfileResults(profile=profile, result=result)
                temp.save()

            return HttpResponse(status=200)
        else:
            profileresults = ProfileResults.objects.all().filter(profile=profile)

            for profileresult in profileresults:
                profileresult.delete()

            return HttpResponse(status=200)

        return HttpResponse(status=500)


class UserList(generics.ListAPIView):
    queryset = User.objects.all()
    serializer_class = UserSerializer

class UserDetail(generics.RetrieveAPIView):
    queryset = User.objects.all()
    serializer_class = UserSerializer

class VisitList(generics.ListCreateAPIView):
    queryset = Visit.objects.all()
    serializer_class = VisitSerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        user = self.request.user
        return Visit.objects.filter(owner=user)

class VisitDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = Visit.objects.all()
    serializer_class = VisitSerializer
    permission_classes = (permissions.IsAuthenticated, IsOwner)

class ResultList(generics.ListCreateAPIView):
    queryset = Result.objects.all()
    serializer_class = ResultSerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        user = self.request.user
        return Result.objects.filter(owner=user)

class ResultDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = Result.objects.all()
    serializer_class = ResultSerializer
    permission_classes = (permissions.IsAuthenticated, IsOwner)

class CategoryList(generics.ListCreateAPIView):
    queryset = Category.objects.all()
    serializer_class = CategorySerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        user = self.request.user
        return Category.objects.filter(owner=user)

class CategoryDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = Category.objects.all()
    serializer_class = CategorySerializer
    permission_classes = (permissions.IsAuthenticated, IsOwner,)

class TestCategoryList(generics.ListCreateAPIView):
    queryset = TestCategory.objects.all()
    serializer_class = TestCategorySerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        user = self.request.user
        return TestCategory.objects.filter(owner=user)

class TestCategoryDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = TestCategory.objects.all()
    serializer_class = TestCategorySerializer
    permission_classes = (permissions.IsAuthenticated, IsOwner,)

class TestProfileList(generics.ListCreateAPIView):
    queryset = TestProfile.objects.all()
    serializer_class = TestProfileSerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        user = self.request.user
        return TestProfile.objects.filter(owner=user)

class TestProfileDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = TestProfile.objects.all()
    serializer_class = TestProfileSerializer
    permission_classes = (permissions.IsAuthenticated, IsOwner,)

class TestList(generics.ListCreateAPIView):
    serializer_class = TestSerializer

    def get_queryset(self):
        username = self.request.user.username
        sex = (int(username[9]) % 2 == 0) and 'K' or 'M'
        return Test.objects.all().filter(Q(sex=sex) | Q(sex=''))

    def pre_save(self, obj):
        obj.owner = self.request.user

class TestDetail(generics.RetrieveAPIView):
    queryset = Test.objects.all()
    serializer_class = TestSerializer

class PlaceList(generics.ListCreateAPIView):
    queryset = Place.objects.all()
    serializer_class = PlaceSerializer

class PlaceDetail(generics.RetrieveAPIView):
    queryset = Place.objects.all()
    serializer_class = PlaceSerializer

class DoctorList(generics.ListCreateAPIView):
    queryset = Doctor.objects.all()
    serializer_class = DoctorSerializer

class DoctorDetail(generics.RetrieveAPIView):
    queryset = Doctor.objects.all()
    serializer_class = DoctorSerializer

class UnitList(generics.ListCreateAPIView):
    queryset = Unit.objects.all()
    serializer_class = UnitSerializer

class UnitDetail(generics.RetrieveAPIView):
    queryset = Unit.objects.all()
    serializer_class = UnitSerializer

class PeriodicTestList(generics.ListCreateAPIView):
    queryset = PeriodicTest.objects.all()
    serializer_class = PeriodicTestSerializer

class PeriodicTestDetail(generics.RetrieveAPIView):
    queryset = PeriodicTest.objects.all()
    serializer_class = PeriodicTestSerializer

class ProfileResultsList(generics.ListCreateAPIView):
    queryset = ProfileResults.objects.all()
    serializer_class = ProfileResultsSerializer

class ProfileResultsDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = ProfileResults.objects.all()
    serializer_class = ProfileResultsSerializer

class ProfileList(generics.ListCreateAPIView):
    queryset = Profile.objects.all()
    serializer_class = ProfileSerializer
    permission_classes = (permissions.IsAuthenticated,)

    def perform_create(self, serializer):
        serializer.save(owner=self.request.user)

    def get_queryset(self):
        user = self.request.user
        return Profile.objects.filter(owner=user)

class ProfileDetail(generics.RetrieveUpdateDestroyAPIView):
    queryset = Profile.objects.all()
    serializer_class = ProfileSerializer
    permission_classes = (permissions.IsAuthenticated, IsOwner,)