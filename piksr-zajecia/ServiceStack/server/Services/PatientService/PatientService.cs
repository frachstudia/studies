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
	public class PatientsService : Service
	{
		public PatientsService ()
		{
			Db.CreateTableIfNotExists<Patient>();
		}
		
		
		public object Get (DtoPatient req)
		{
			var patients = req.Id == null
				? Db.Select<Patient> ()
					: Db.Select<Patient> (q => q.Id == req.Id);
			
			var dtoPatients = patients.Select (p => new DtoPatient ().PopulateWith (p)).ToList ();
			
			return new DtoPatientResponse (dtoPatients);
		}
		
		
		public object Delete (DtoPatient req)
		{
			var found = Db.Select<Patient> (q => q.Id == req.Id);
			if (found.Count == 0)
			return new HttpResult {StatusCode = HttpStatusCode.NotFound};
			
			Db.DeleteById<Patient> (req.Id);
			
			return new HttpResult {StatusCode = HttpStatusCode.NoContent};
		}
		
		
	}
}