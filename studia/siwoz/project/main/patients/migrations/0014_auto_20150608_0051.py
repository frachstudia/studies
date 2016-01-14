# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations
from django.conf import settings


class Migration(migrations.Migration):

    dependencies = [
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('patients', '0013_auto_20150607_2155'),
    ]

    operations = [
        migrations.CreateModel(
            name='Profile',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('name', models.CharField(max_length=32)),
                ('hash', models.CharField(max_length=32)),
                ('owner', models.ForeignKey(related_name='profiles', to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.RenameModel(
            old_name='HashResults',
            new_name='ProfileResults',
        ),
        migrations.RemoveField(
            model_name='hash',
            name='owner',
        ),
        migrations.DeleteModel(
            name='Hash',
        ),
    ]
