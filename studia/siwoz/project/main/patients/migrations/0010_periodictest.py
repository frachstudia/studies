# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations
from django.conf import settings


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('patients', '0009_auto_20150531_2103'),
    ]

    operations = [
        migrations.CreateModel(
            name='PeriodicTest',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('startdate', models.DateTimeField()),
                ('intervalvalue', models.IntegerField()),
                ('intervalperiod', models.CharField(max_length=10)),
                ('comment', models.TextField(default=None, null=True, blank=True)),
                ('preparation', models.TextField(default=None, null=True, blank=True)),
                ('owner', models.ForeignKey(related_name='periodictests', to=settings.AUTH_USER_MODEL)),
                ('place', models.ForeignKey(default=None, blank=True, to='patients.Place', null=True)),
                ('test', models.ForeignKey(to='patients.Test')),
            ],
        ),
    ]
