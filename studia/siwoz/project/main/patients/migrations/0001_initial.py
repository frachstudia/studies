# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations
from django.conf import settings


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
    ]

    operations = [
        migrations.CreateModel(
            name='Category',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=100)),
                ('owner', models.ForeignKey(related_name='categorys', to=settings.AUTH_USER_MODEL)),
            ],
            options={
                'ordering': ['name'],
            },
        ),
        migrations.CreateModel(
            name='Doctor',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=100)),
                ('speciality', models.CharField(default=None, max_length=100, blank=True)),
            ],
            options={
                'ordering': ['name'],
            },
        ),
        migrations.CreateModel(
            name='Place',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=200)),
                ('address', models.CharField(default=None, max_length=120, blank=True)),
                ('telnumber', models.CharField(default=None, max_length=15, blank=True)),
            ],
            options={
                'ordering': ['name'],
            },
        ),
        migrations.CreateModel(
            name='Result',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('date', models.DateField()),
                ('result', models.DecimalField(default=None, null=True, max_digits=9, decimal_places=4, blank=True)),
                ('resultdesc', models.TextField(default=None, blank=True)),
                ('owner', models.ForeignKey(related_name='results', to=settings.AUTH_USER_MODEL)),
                ('place', models.ForeignKey(default=None, blank=True, to='patients.Place', null=True)),
            ],
            options={
                'ordering': ['date'],
            },
        ),
        migrations.CreateModel(
            name='Test',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=100)),
                ('sex', models.CharField(default=None, max_length=1, blank=True)),
                ('refmin', models.DecimalField(default=None, max_digits=9, decimal_places=4, blank=True)),
                ('refmax', models.DecimalField(default=None, max_digits=9, decimal_places=4, blank=True)),
            ],
            options={
                'ordering': ['name'],
            },
        ),
        migrations.CreateModel(
            name='TestCategory',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('category', models.ForeignKey(to='patients.Category')),
                ('owner', models.ForeignKey(related_name='testcategorys', to=settings.AUTH_USER_MODEL)),
                ('test', models.ForeignKey(to='patients.Test')),
            ],
        ),
        migrations.CreateModel(
            name='Unit',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=100)),
            ],
            options={
                'ordering': ['name'],
            },
        ),
        migrations.CreateModel(
            name='Visit',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=100)),
                ('date', models.DateField()),
                ('comment', models.TextField(default=None, blank=True)),
                ('preparation', models.TextField(default=None, blank=True)),
                ('doctor', models.ForeignKey(default=None, blank=True, to='patients.Doctor', null=True)),
                ('owner', models.ForeignKey(related_name='visits', to=settings.AUTH_USER_MODEL)),
                ('place', models.ForeignKey(default=None, blank=True, to='patients.Place', null=True)),
            ],
            options={
                'ordering': ['date'],
            },
        ),
        migrations.AddField(
            model_name='test',
            name='unit',
            field=models.ForeignKey(to='patients.Unit'),
        ),
        migrations.AddField(
            model_name='result',
            name='test',
            field=models.ForeignKey(to='patients.Test'),
        ),
    ]
