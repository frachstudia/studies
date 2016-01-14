using System;
using System.Data;
using ServiceStack.CacheAccess;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using Server.Logic.Patients;

namespace Server.Services
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public IDbConnectionFactory DbFactory { get; set; }
        public ICacheClient CacheClient { get; set; }

        public bool PatientWithIdExists(int id)
        {
            using (var c = DbFactory.OpenDbConnection())
            {
                var patients = c.Select<Patient>(q => q.Id == id);
                return patients.Count > 0;
            }
        }
    }
}

