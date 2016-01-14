using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TechPoznanNaprawy
{
    [Serializable]
    public class Client
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string code { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string telnum { get; set; }

        public Client() { }

        public Client(string id, string name, string addr, string cod, string cit, string mail, string tel)
        {
            this.id = id;
            this.name = name;
            this.address = addr;
            this.code = cod;
            this.city = cit;
            this.email = mail;
            this.telnum = tel;
        }

        public static Client getClientFromList(string name, List<Client> clients)
        {
            foreach (Client client in clients)
            {
                if (client.name == name)
                    return client;
            }

            return null;
        }
    }
}
