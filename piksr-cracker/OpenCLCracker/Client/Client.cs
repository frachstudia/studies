using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Server.Logic;
using server.Services.NodeService;
using server.Services;
using Common;

namespace Client
{
	public class Client {
		public Client ()
		{
			Console.Write("Login: ");
			this.login = Console.ReadLine();
			this.password = takePassword();
		}

		public int id;
		public int hashCount = 0;
		public string type { get; set; }
		public string login { get; set; }
		public SecureString password { get; set; }

		private SecureString takePassword ()
		{
			SecureString result = new SecureString();

			Console.Write("Password: ");
			while (true)
			{
				ConsoleKeyInfo i = Console.ReadKey(true);
				if (i.Key == ConsoleKey.Enter)
				{
					break;
				}
				else if (i.Key == ConsoleKey.Backspace)
				{
					if (result.Length > 0)
					{
						result.RemoveAt(result.Length - 1);
						Console.Write("\b \b");
					}
				}
				else if (i.Key == ConsoleKey.Spacebar || result.Length > 20) {}
				else
				{
					result.AppendChar(i.KeyChar);
					Console.Write("*");
				}
			}
			Console.WriteLine();

			return result;
		}

		private string convertToString(SecureString secstrPassword)
		{
			IntPtr unmanagedString = IntPtr.Zero;
			try
			{
				unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
				return Marshal.PtrToStringUni(unmanagedString);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
			}
		}

		public void crack(string hash, string type, string maxLength)
		{
			Hash newHash = new Hash {
				hash = hash, 
				sum = type, 
				maxLength = int.Parse (maxLength), 
				id = Environment.MachineName + "_" + this.hashCount++
			};
			string json = "{ \"id\": \"" + newHash.id + "\", \"hash\": \"" +
				newHash.hash + "\", \"sum\": \"" + newHash.sum + "\", \"maxLength\": " +
				newHash.maxLength + "}";

			//Console.WriteLine (json);
			ASCIIEncoding encoding = new ASCIIEncoding ();
			byte[] byteArr = encoding.GetBytes (json);

			try
			{
				HttpWebRequest request = WebRequest.Create(@"http://localhost:8888/api/hash") as HttpWebRequest;
				request.Method = "POST";
				request.ContentType = "application/json";
				request.Accept = "application/json";
				request.ContentLength = byteArr.Length;
				request.Headers["Authorization"] = "Basic " + 
					Convert.ToBase64String(Encoding.Default.GetBytes(login + ":" + convertToString(password)));

				using (var streamWriter = new StreamWriter(request.GetRequestStream()))
				{
					streamWriter.Write(json);
				}

				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					DataContractJsonSerializer jsonSerializer = 
						new DataContractJsonSerializer(typeof(DtoHashIDResponse));
					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
					DtoHashIDResponse jsonResponse = objResponse as DtoHashIDResponse;

					//if (jsonResponse.msg.Item2.Length > 0)
					Console.WriteLine("Job started. Job ID = " + jsonResponse.id);
					//else
					//	Console.WriteLine("Job hasn't started. Unknown error.");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void getStatus(string id)
		{
			try
			{
				HttpWebRequest request = WebRequest.Create(@"http://localhost:8888/api/hash/" + id) as HttpWebRequest;
				request.Method = "GET";
				request.ContentType = "application/json";
				request.Accept = "application/json";
				request.Headers["Authorization"] = "Basic " + 
					Convert.ToBase64String(Encoding.Default.GetBytes(login + ":" + convertToString(password)));

				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					DataContractJsonSerializer jsonSerializer = 
						new DataContractJsonSerializer(typeof(DtoHashStatusResponse));
					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
					DtoHashStatusResponse jsonResponse = objResponse as DtoHashStatusResponse;

					switch (jsonResponse.result[0]) {
					case "PROCESSING":
						Console.WriteLine("Still working.");
						break;
					case "SOLVED":
						Console.WriteLine("Password is: " + jsonResponse.result[1]);
						break;
					default:
						Console.WriteLine(jsonResponse.result[0]);
						break;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void addNode(string addr, string port) 
		{
			Node newNode = new Node{ address = addr, port = int.Parse(port), id = addr + "_" + port };
			string json = "{ \"address\": \"" + 
				newNode.address + "\", \"port\": " + newNode.port + ", \"id\": \"" +
				newNode.id + "\"}";
			//Console.WriteLine (json);
			ASCIIEncoding encoding = new ASCIIEncoding ();
			byte[] byteArr = encoding.GetBytes (json);
			try
			{
				HttpWebRequest request = WebRequest.Create(@"http://localhost:8888/api/nodes/" + 
					newNode.id) as HttpWebRequest;
				request.Method = "POST";
				request.ContentType = "application/json";
				request.Accept = "application/json";
				request.ContentLength = byteArr.Length;
				request.Headers["Authorization"] = "Basic " + 
					Convert.ToBase64String(Encoding.Default.GetBytes(login + ":" + convertToString(password)));

				using (var streamWriter = new StreamWriter(request.GetRequestStream()))
				{
					streamWriter.Write(json);
				}

				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode == HttpStatusCode.Conflict)
						Console.WriteLine("This node is already on server.");
					else if (response.StatusCode == HttpStatusCode.Created)
						Console.WriteLine("Node has been added.");
					else 
						Console.WriteLine("Node wasn't added (unknown error).");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void deleteNode(string addr, string port)
		{
			Node newNode = new Node{ address = addr, port = int.Parse(port), id = addr + "_" + port };
			/*string json = "{ \"address\": \"" + 
				newNode.address + "\", \"port\": " + newNode.port + ", \"id\": \"" +
				newNode.id + "\" }";
			Console.WriteLine (json);
			ASCIIEncoding encoding = new ASCIIEncoding ();
			byte[] byteArr = encoding.GetBytes (json);*/
			try
			{
				HttpWebRequest request = WebRequest.Create(@"http://localhost:8888/api/nodes/" + 
					newNode.id) as HttpWebRequest;
				request.Method = "DELETE";
				request.ContentType = "application/json";
				request.Accept = "application/json";
				request.ContentLength = 0;
				request.Headers["Authorization"] = "Basic " + 
					Convert.ToBase64String(Encoding.Default.GetBytes(login + ":" + convertToString(password)));

				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode == HttpStatusCode.OK)
						Console.WriteLine("Node has been deleted.");
					else 
						Console.WriteLine("Node hasn't been deleted (unknown error).");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void getNodes()
		{
			try
			{
				HttpWebRequest request = WebRequest.Create(@"http://localhost:8888/api/nodes") as HttpWebRequest;
				request.Method = "GET";
				request.ContentType = "application/json";
				request.Accept = "application/json";
				request.Headers["Authorization"] = "Basic " + 
					Convert.ToBase64String(Encoding.Default.GetBytes(login + ":" + convertToString(password)));

				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						throw new Exception(String.Format(
							"Server error (HTTP {0}: {1}).",
							response.StatusCode,
							response.StatusDescription));
					DataContractJsonSerializer jsonSerializer = 
						new DataContractJsonSerializer(typeof(DtoNodeResponse));
					object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
					DtoNodeResponse jsonResponse = objResponse as DtoNodeResponse;

					if (jsonResponse.Nodes.Count == 0)
						Console.WriteLine("No nodes.");
					else
						foreach (DtoNode node in jsonResponse.Nodes) {
							Console.WriteLine(node.id);
						}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}