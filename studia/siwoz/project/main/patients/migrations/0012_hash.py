# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations
from django.conf import settings


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('patients', '0011_hashresults'),
    ]

    operations = [
        migrations.CreateModel(
            name='Hash',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('hashname', models.CharField(max_length=32)),
                ('hash', models.CharField(max_length=32)),
                ('owner', models.ForeignKey(related_name='hashes', to=settings.AUTH_USER_MODEL)),
            ],
        ),
    ]
