# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0010_periodictest'),
    ]

    operations = [
        migrations.CreateModel(
            name='HashResults',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('hash', models.CharField(max_length=32)),
                ('result', models.ForeignKey(to='patients.Result')),
            ],
        ),
    ]
