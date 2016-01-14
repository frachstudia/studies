#-*- coding: utf-8 -*-

from models import *
from rest_framework import serializers
from django.contrib.auth.models import User


class UserSerializer(serializers.ModelSerializer):
    visits = serializers.PrimaryKeyRelatedField(many=True, queryset=Visit.objects.all())
    categorys = serializers.PrimaryKeyRelatedField(many=True, queryset=Category.objects.all())
    testcategorys = serializers.PrimaryKeyRelatedField(many=True, queryset=TestCategory.objects.all())
    results = serializers.PrimaryKeyRelatedField(many=True, queryset=Result.objects.all())

    class Meta:
        model = User
        fields = ('id', 'username', 'visits', 'categorys', 'testcategorys', 'results')


class VisitSerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = Visit
        fields = ('id', 'name', 'date', 'place', 'doctor', 'comment', 'preparation', 'owner')


class CategorySerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = Category
        fields = ('id', 'name', 'owner')


class TestCategorySerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = TestCategory
        fields = ('id', 'test', 'category', 'owner')


class TestProfileSerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = TestProfile
        fields = ('id', 'test', 'profile', 'owner')


class ResultSerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = Result
        fields = ('id', 'test', 'date', 'place', 'result', 'resulttype', 'comment', 'owner')


class PeriodicTestSerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = PeriodicTest
        fields = ('id', 'test', 'startdate', 'intervalvalue', 'intervalperiod', 'place', 'comment', 'preparation', 'owner')


class TestSerializer(serializers.ModelSerializer):
    class Meta:
        model = Test
        fields = ('id', 'fullname', 'sex', 'name', 'unit', 'refmin', 'refmax')


class UnitSerializer(serializers.ModelSerializer):
    class Meta:
        model = Unit
        fields = ('id', 'name')


class PlaceSerializer(serializers.ModelSerializer):
    class Meta:
        model = Place
        fields = ('id', 'name', 'address', 'telnumber')


class DoctorSerializer(serializers.ModelSerializer):
    class Meta:
        model = Doctor
        fields = ('id', 'name', 'speciality')

class ProfileResultsSerializer(serializers.ModelSerializer):
    class Meta:
        model = ProfileResults
        fields = ('id', 'profile', 'result')

class ProfileSerializer(serializers.ModelSerializer):
    owner = serializers.ReadOnlyField(source='owner.username')

    class Meta:
        model = Profile
        fields = ('id', 'name', 'hash', 'owner')