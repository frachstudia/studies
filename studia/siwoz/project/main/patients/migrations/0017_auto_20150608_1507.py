# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0016_auto_20150608_1046'),
    ]

    operations = [
        migrations.AlterField(
            model_name='profile',
            name='hash',
            field=models.CharField(max_length=33),
        ),
    ]
