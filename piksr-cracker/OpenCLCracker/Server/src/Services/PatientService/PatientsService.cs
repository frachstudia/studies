using System;
using System.Linq;
using System.Net;
using Server.Logic.Patients;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;

namespace Server.Services.PatientService
{
    [Authenticate]
    public class PatientsService : Service
    {
        public PatientsService ()
        {
            Db.CreateTableIfNotExists<Patient>();
        }

        [RequiredPermission("read")]
        public object Get (DtoPatient req)
        {
            var patients = req.Id == null
                ? Db.Select<Patient> ()
                : Db.Select<Patient> (q => q.Id == req.Id);

            var dtoPatients = patients.Select (p => new DtoPatient ().PopulateWith (p)).ToList ();

            return new DtoPatientResponse (dtoPatients);
        }

        [RequiredRole("editor")]
        public object Post(DtoPatient req)
        {
            var patient = Db.Select<Patient>(q => q.Id == req.Id).SingleOrDefault();
            if (patient != null)
                return new HttpResult { StatusCode = HttpStatusCode.Conflict };

            var newPatient = new Patient().PopulateWith(req);
            Db.Insert(newPatient);

            long id = Db.GetLastInsertId();

            return new HttpResult
            {
                StatusCode = HttpStatusCode.Created,
                Headers = {
                    { HttpHeaders.Location, string.Format("/api/patient/{0}", id) }
                }
            };
        }

        [RequiredRole("editor")]
        public object Delete (DtoPatient req)
        {
            var found = Db.Select<Patient> (q => q.Id == req.Id);
            if (found.Count == 0)
                return new HttpResult {StatusCode = HttpStatusCode.NotFound};

            Db.DeleteById<Patient> (req.Id);

            return new HttpResult {StatusCode = HttpStatusCode.NoContent};
        }

        [RequiredRole("editor")]
        public object Put(DtoPatient req)
        {
            var existing = Db.Select<Patient>(q => q.Id == req.Id).SingleOrDefault();
            if (existing == null)
                return new HttpResult {StatusCode = HttpStatusCode.NotFound};
            var updated = new Patient().PopulateWith(req);
            Db.Update(updated);
            var dtoPatients = new DtoPatient[] { new DtoPatient().PopulateWith(updated) };
            return new DtoPatientResponse(dtoPatients);
        }
    }
}
