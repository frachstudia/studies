using System;
using System.Text.RegularExpressions;

namespace Client
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Client client = new Client();

			String[] command;
			Boolean quitNow = false;
			while(!quitNow)
			{
				Console.Write ("?> ");
				command = Console.ReadLine().Split(' ');
				switch (command[0])
				{
				case "node":
					if (command.Length < 3 && command [1] != "list") {
						Console.WriteLine ("Wrong arguments.");
						break;
					}
					if (command [1] == "add") {
						if (command.Length != 4 || !Regex.IsMatch (command [3], @"^\d+$") || int.Parse (command [3]) > 65536) {
							Console.WriteLine ("Wrong arguments.");
						} else {
							//Console.WriteLine ("Wysyłam add(" + command [2] + ":" + command [3] + ")");
							client.addNode(command[2], command[3]);
						}
					} else if (command [1] == "del") {
						if (command.Length != 4 || !Regex.IsMatch (command [3], @"^\d+$") || int.Parse (command [3]) > 65536) {
							Console.WriteLine ("Wrong arguments.");
						} else {
							//Console.WriteLine ("Wysyłam del(" + command [2] + ":" + command [3] + ")");
							client.deleteNode(command[2], command[3]);
						}
					} else if (command [1] == "list") {
						//Console.WriteLine ("Wysyłam list()");
						client.getNodes();
					} else {
						Console.WriteLine ("Wrong arguments.");
					}
					break;
				case "hash":
					if (command.Length != 4 || !Regex.IsMatch (command [3], @"^\d+$") ||
						(!Regex.IsMatch (command [2], @"^md5$") && !Regex.IsMatch (command [2], @"^sha1$"))) {
						Console.WriteLine ("Wrong arguments.");
						break;
					}
					//Console.WriteLine ("Wysyłam hash(" + command [1] + ", " + command [2] + ", " + command [3] + ")");
					client.crack (command [1], command [2], command [3]);
					break;
				case "status":
					if (command.Length != 2) {
						Console.WriteLine ("Wrong arguments.");
						break;
					}
					client.getStatus (command [1]);
					break;
				case "exit":
					quitNow = true;
					Console.WriteLine ("Exiting");
					break;
				case "":
					break;
				default:
					Console.WriteLine ("Wrong command.");
					break;
				}	
			}
		}
	}
}