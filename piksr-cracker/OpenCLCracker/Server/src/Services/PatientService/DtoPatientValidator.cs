using System;
using System.Text.RegularExpressions;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

namespace Server.Services.PatientService
{
    public class DtoPatientValidator : BaseValidator<DtoPatient>

    {
        public DtoPatientValidator()
        {
            RuleSet(ApplyTo.Get, () =>
            {
                RuleFor(r => r.Id).Must(id => id == null || PatientWithIdExists(id.Value));
            });
            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(r => r.Id).Must(id => id == null);
            });
            RuleSet(ApplyTo.Put | ApplyTo.Delete, () =>
            {
                RuleFor(r => r.Id).Must(id => PatientWithIdExists(id.Value));
            });
            RuleSet(ApplyTo.Put | ApplyTo.Post, () =>
            {
                RuleFor(r => r.FirstName).NotEmpty();
                RuleFor(r => r.LastName).NotEmpty();
                RuleFor(r => r.ContactPhoneNumber).Must(number => Regex.IsMatch(number, @"^[0-9- ]+$"));
            });
        }
    }
}

