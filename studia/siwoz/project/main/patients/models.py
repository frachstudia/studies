#-*- coding: utf-8 -*-

from django.db import models
from django.contrib.auth.models import AbstractUser


class Unit(models.Model):
    name = models.CharField(max_length=100)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

    class Meta:
        ordering = ['name']


class Test(models.Model):
    name = models.CharField(max_length=100)
    fullname = models.CharField(max_length=100, blank=True, default=None)
    sex = models.CharField(max_length=1, blank=True, default=None)
    unit = models.ForeignKey(Unit)
    refmin = models.DecimalField(max_digits=9, decimal_places=4, blank=True, default=None)
    refmax = models.DecimalField(max_digits=9, decimal_places=4, blank=True, default=None)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

    class Meta:
        ordering = ['name']


class Doctor(models.Model):
    name = models.CharField(max_length=100)
    speciality = models.CharField(max_length=100, blank=True, default=None)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

    class Meta:
        ordering = ['name']


class Place(models.Model):
    name = models.CharField(max_length=300)
    address = models.CharField(max_length=120, blank=True, default=None)
    telnumber = models.CharField(max_length=15, blank=True, default=None)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

    class Meta:
        ordering = ['name']


class Result(models.Model):
    owner = models.ForeignKey('auth.User', related_name='results')
    test = models.ForeignKey(Test)
    date = models.DateField()
    place = models.ForeignKey(Place, null=True, blank=True, default=None)
    result = models.CharField(max_length=1000, default='0')
    resulttype = models.CharField(max_length=11, default='numeric')
    comment = models.TextField(blank=True, default=None)

    def __str__(self):
        return self.test

    def __unicode__(self):
        return unicode(self.test)

    class Meta:
        ordering = ['date']


class Visit(models.Model):
    owner = models.ForeignKey('auth.User', related_name='visits')
    name = models.CharField(max_length=100)
    date = models.DateTimeField()
    place = models.ForeignKey(Place, null=True, blank=True, default=None)
    doctor = models.ForeignKey(Doctor, null=True, blank=True, default=None)
    comment = models.TextField(null=True, blank=True, default=None)
    preparation = models.TextField(null=True, blank=True, default=None)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

    class Meta:
        ordering = ['date']


class Category(models.Model):
    owner = models.ForeignKey('auth.User', related_name='categorys')
    name = models.CharField(max_length=100)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

    class Meta:
        ordering = ['name']


class TestCategory(models.Model):
    owner = models.ForeignKey('auth.User', related_name='testcategorys')
    test = models.ForeignKey(Test)
    category = models.ForeignKey(Category)

class PeriodicTest(models.Model):
    owner = models.ForeignKey('auth.User', related_name='periodictests')
    test = models.ForeignKey(Test)
    startdate = models.DateTimeField()
    intervalvalue = models.IntegerField()
    intervalperiod = models.CharField(max_length=10)
    place = models.ForeignKey(Place, null=True, blank=True, default=None)
    comment = models.TextField(null=True, blank=True, default=None)
    preparation = models.TextField(null=True, blank=True, default=None)

    def __str__(self):
        return self.test.name

    def __unicode__(self):
        return unicode(self.test.name)

class Profile(models.Model):
    owner = models.ForeignKey('auth.User', related_name='profiles')
    name = models.CharField(max_length=32)
    hash = models.CharField(max_length=32)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)

class ProfileResults(models.Model):
    profile = models.ForeignKey(Profile, null=True)
    result = models.ForeignKey(Result)

    def __str__(self):
        return self.profile.name

    def __unicode__(self):
        return unicode(self.profile.name)

class TestProfile(models.Model):
    owner = models.ForeignKey('auth.User', related_name='testprofiles')
    test = models.ForeignKey(Test)
    profile = models.ForeignKey(Profile)