from django.db import models


class Test(models.Model):
    name = models.CharField(max_length=100)
    unit = models.CharField(max_length=100)
    refmin = models.DecimalField(max_digits=9, decimal_places=4)
    refmax = models.DecimalField(max_digits=9, decimal_places=4)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)


class Doctor(models.Model):
    name = models.CharField(max_length=100)
    speciality = models.CharField(max_length=100)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)


class Place(models.Model):
    name = models.CharField(max_length=200)
    address = models.CharField(max_length=120)
    telnumber = models.CharField(max_length=15)

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)


class Results(models.Model):
    test = models.ForeignKey(Test)
    date = models.DateField()
    result = models.DecimalField(max_digits=9, decimal_places=4)
    resultdesc = models.TextField()


class Visits(models.Model):
    name = models.CharField(max_length=100)
    date = models.DateField()
    place = models.ForeignKey(Place)
    doctor = models.ForeignKey(Doctor)
    comment = models.TextField()
    preparation = models.TextField()

    def __str__(self):
        return self.name

    def __unicode__(self):
        return unicode(self.name)