# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0015_testprofile'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='profileresults',
            name='hash',
        ),
        migrations.AddField(
            model_name='profileresults',
            name='profile',
            field=models.ForeignKey(to='patients.Profile', null=True),
        ),
    ]
