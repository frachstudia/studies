# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations
from django.conf import settings


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('patients', '0014_auto_20150608_0051'),
    ]

    operations = [
        migrations.CreateModel(
            name='TestProfile',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('owner', models.ForeignKey(related_name='testprofiles', to=settings.AUTH_USER_MODEL)),
                ('profile', models.ForeignKey(to='patients.Profile')),
                ('test', models.ForeignKey(to='patients.Test')),
            ],
        ),
    ]
