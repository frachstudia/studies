# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('patients', '0012_hash'),
    ]

    operations = [
        migrations.RenameField(
            model_name='hash',
            old_name='hashname',
            new_name='name',
        ),
    ]
