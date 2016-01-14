# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0005_auto_20150528_0732'),
    ]

    operations = [
        migrations.AlterField(
            model_name='result',
            name='resultdesc',
            field=models.TextField(default=None, null=True, blank=True),
        ),
    ]
