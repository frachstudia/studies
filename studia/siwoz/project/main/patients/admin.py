#-*- coding: utf-8 -*-

from django.contrib import admin
from patients.models import *

# Register your models here.
admin.site.register(Unit)
admin.site.register(Category)
admin.site.register(Place)
admin.site.register(Doctor)
admin.site.register(Test)
admin.site.register(Visit)
admin.site.register(Result)