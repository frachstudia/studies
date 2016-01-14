# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0008_auto_20150531_1907'),
    ]

    operations = [
        migrations.AlterField(
            model_name='visit',
            name='comment',
            field=models.TextField(default=None, null=True, blank=True),
        ),
        migrations.AlterField(
            model_name='visit',
            name='preparation',
            field=models.TextField(default=None, null=True, blank=True),
        ),
    ]
