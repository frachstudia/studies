#-*- coding: utf-8 -*-

from django import forms
from django.contrib.auth import get_user_model
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm, UserChangeForm
from django.core.validators import ValidationError
from models import *

class MyRegistrationForm(UserCreationForm):
    username = forms.RegexField(r"^[0-9]{11}$", label=("PESEL"), widget=forms.TextInput(attrs={'placeholder': 'Wpisz PESEL','class': 'form-control'}),
                                error_messages={
                                    'invalid': 'Wpisz poprawny PESEL.',
                                    'required': 'Wpisz PESEL.',
                                    'duplicate_username': 'Ten PESEL jest już w bazie.'
                                })
    password1 = forms.CharField(widget=forms.PasswordInput(attrs={'placeholder': 'Wpisz hasło', 'class': 'form-control'}),
                                error_messages={
                                    'required': 'Uzupełnij pole z hasłem.',
                                    'password_mismatch': 'Hasła nie są identyczne',
                                })
    password2 = forms.CharField(widget=forms.PasswordInput(attrs={'placeholder': 'Powtórz hasło', 'class': 'form-control'}),
                                error_messages={
                                    'required': 'Uzupełnij pole z powtórzonym hasłem.',
                                    'password_mismatch': 'Hasła nie są identyczne.',
                                })

    class Meta:
        model = User
        fields = ('username', 'password1', 'password2')

    def save(self, commit=True):
        user = super(MyRegistrationForm, self).save(commit=False)

        if commit:
            user.save()

        return user

    # Validate username form field
    def clean_username(self):
        username = self.cleaned_data['username']

        wages = '1379137913'
        sum = 0

        for i in range(0,10):
            sum += int(wages[i]) * int(username[i])

        kontrola = sum % 10

        print kontrola

        if kontrola != 0:
            kontrola = 10 - kontrola

        if kontrola != int(username[10]):
            raise forms.ValidationError("Niepoprawny PESEL.")

        try:
            get_user_model().objects.get(username=username)
        except get_user_model().DoesNotExist:
            return username
        raise forms.ValidationError("Ten PESEL jest już zajęty.")

        return username


class ResultForm(forms.ModelForm):
    class Meta:
        model = Result
        fields = ['test', 'date', 'place', 'result', 'resulttype']


class CategoryForm(forms.ModelForm):
    class Meta:
        model = Category
        fields = ['name']


class VisitForm(forms.ModelForm):
    class Meta:
        model = Visit
        fields = ['name', 'date', 'place', 'doctor', 'comment', 'preparation']


class TestCategoryForm(forms.ModelForm):
    class Meta:
        model = TestCategory
        fields = ['test', 'category']