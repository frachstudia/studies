# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0002_result_comment'),
    ]

    operations = [
        migrations.AddField(
            model_name='test',
            name='fullname',
            field=models.CharField(default=None, max_length=1, blank=True),
        ),
    ]
