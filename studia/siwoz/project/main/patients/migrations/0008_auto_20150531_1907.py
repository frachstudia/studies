# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0007_auto_20150528_0838'),
    ]

    operations = [
        migrations.AlterField(
            model_name='visit',
            name='date',
            field=models.DateTimeField(),
        ),
    ]
