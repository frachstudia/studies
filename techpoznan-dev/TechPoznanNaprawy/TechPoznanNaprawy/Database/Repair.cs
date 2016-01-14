using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using TechPoznanNaprawy.Database;

namespace TechPoznanNaprawy
{
    [Serializable]
    public class Repair
    {
        public string id { get; set; }
        public DateTime date { get; set; }
        public string client { get; set; }
        public DateTime pricingDate { get; set; }
        public string pricingComment { get; set; }
        public string state { get; set; }
        public string archive { get; set; }

        public List<Item> items = new List<Item>();

        public Repair()
        {
        }

        public Repair(string id, DateTime date, string client, DateTime pricingDate, string state, string archive, string pricingComment)
        {
            this.id = id;
            this.date = date;
            this.client = client;
            this.pricingDate = pricingDate;
            this.state = state;
            this.archive = archive;
            this.pricingComment = pricingComment;
        }

        public string getItemsShortcut()
        {
            string result = "";

            foreach (Item i in items)
            {
                result += i.item;
                result += " ||| ";
            }

            // Cutting unused postfix
            result = result.Substring(0, result.Length - 5);

            return result;
        }

        public static Repair getRepairFromList(string id, List<Repair> repairs)
        {
            foreach (Repair repair in repairs)
            {
                if (repair.id == id)
                    return repair;
            }

            return null;
        }
    }
}