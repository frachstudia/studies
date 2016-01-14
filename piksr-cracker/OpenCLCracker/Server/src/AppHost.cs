using System;
using System.Collections.Generic;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using ServiceStack.Redis;
using servstack;

namespace Server
{
	public class AppHost : AppHostHttpListenerBase
	{
		private readonly bool m_debugEnabled = true;

		public AppHost ()
			: base ("Server HttpListener", typeof(AppHost).Assembly)
		{

		}
        
		public override void Configure (Container container)
		{
			LogManager.LogFactory = new NLogFactory ();
			this.RequestFilters.Add ((req, resp, requestDto) => {
				ILog log = LogManager.GetLogger (GetType ());
				log.Info (string.Format (
					"REQ {0}: {1} {2} {3} {4} {5}\n",
					DateTimeOffset.Now.Ticks, req.HttpMethod,
					req.OperationName, req.RemoteIp, req.RawUrl, req.UserAgent));
			});
			this.RequestFilters.Add ((req, resp, requestDto) => {
				ILog log = LogManager.GetLogger (GetType ());
				log.Info (string.Format (
					"RES {0}: {1} {2}\n",
					DateTimeOffset.Now.Ticks, resp.StatusCode, resp.ContentType));
			});

			JsConfig.DateHandler = JsonDateHandler.ISO8601;
            
			Plugins.Add (new AuthFeature (() => new AuthUserSession (),
				new IAuthProvider[] { new BasicAuthProvider () })
			);
			Plugins.Add (new RegistrationFeature ());
			Plugins.Add (new RequestLogsFeature ());
            
			container.RegisterAutoWiredAs<Scheduler, IScheduler> ();

			container.Register<ICacheClient> (new MemoryCacheClient ());            
			container.Register<IDbConnectionFactory> (new OrmLiteConnectionFactory 
				(@"Data Source=db.sqlite;Version=3;", SqliteOrmLiteDialectProvider.Instance));
            
			//Use OrmLite DB Connection to persist the UserAuth and AuthProvider info
			container.Register<IUserAuthRepository> (c => new OrmLiteAuthRepository (c.Resolve<IDbConnectionFactory> ()));

			Plugins.Add (new ValidationFeature ());
			container.RegisterValidators (typeof(AppHost).Assembly);
            
			var config = new EndpointHostConfig ();
            
			if (m_debugEnabled) {
				config.DebugMode = true; //Show StackTraces in service responses during development
				config.WriteErrorsToResponse = true;
				config.ReturnsInnerException = true;
			}

			container.AutoWire (this);
            
			SetConfig (config);
			CreateMissingTables (container);
		}

		public IScheduler Scheduler { get; set; }
			        
		private void CreateMissingTables (Container container)
		{
			var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository> ();
			authRepo.CreateMissingTables ();
			/*
			string salt;
			string hash;
			new SaltedHash().GetHashAndSaltString("admin", out hash, out salt);

			authRepo.CreateUserAuth(new UserAuth
				{
					Id = 1,
					DisplayName = "Administrator",
					UserName = "admin",
					PasswordHash = hash,
					Salt = salt,
					Roles = new List<string> { "Admin", "Normal" },
					Permissions = new List<string> { "noding", "hashing" }
				}, "admin");

			new SaltedHash().GetHashAndSaltString("node1", out hash, out salt);
			authRepo.CreateUserAuth(new UserAuth
				{
					Id = 2,
					DisplayName = "NormalNode1",
					UserName = "node1",
					PasswordHash = hash,
					Salt = salt,
					Roles = new List<string> { "Normal" },
					Permissions = new List<string> { "hashing" }
				}, "node1");

			new SaltedHash().GetHashAndSaltString("node2", out hash, out salt);
			authRepo.CreateUserAuth(new UserAuth
				{
					Id = 3,
					DisplayName = "NormalNode2",
					UserName = "node2",
					PasswordHash = hash,
					Salt = salt,
					Roles = new List<string> { "Normal" },
					Permissions = new List<string> { "hashing" }
				}, "node2");*/
		}
	}
}