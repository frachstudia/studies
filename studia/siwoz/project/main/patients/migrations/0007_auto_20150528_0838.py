# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0006_auto_20150528_0829'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='result',
            name='resultdesc',
        ),
        migrations.AddField(
            model_name='result',
            name='resulttype',
            field=models.CharField(default=b'numeric', max_length=11),
        ),
        migrations.AlterField(
            model_name='result',
            name='result',
            field=models.CharField(default=b'0', max_length=1000),
        ),
    ]
