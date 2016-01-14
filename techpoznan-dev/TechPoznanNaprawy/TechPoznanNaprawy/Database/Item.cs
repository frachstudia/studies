using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechPoznanNaprawy.Database
{
    [Serializable]
    public class Item
    {
        public string repairId { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string item { get; set; }
        public string fabrnum { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string decision { get; set; }
        public DateTime decisionDate { get; set; }
        public string receiver { get; set; }
        public DateTime receiveDate { get; set; }

        public Item() { }

        public Item(Item item)
        {
            this.repairId = item.repairId;
            this.id = item.id;
            this.type = item.type;
            this.item = item.item;
            this.description = item.description;
            this.price = item.price;
            this.decision = item.decision;
            this.decisionDate = item.decisionDate;
            this.receiver = item.receiver;
            this.receiveDate = item.receiveDate;
        }

        public Item(string repairId, string id, string type, string item, string description, string price, string decision,
            DateTime decisionDate, string receiver, DateTime receiveDate, string fabrnum)
        {
            this.repairId = repairId;
            this.id = id;
            this.type = type;
            this.item = item;
            this.description = description;
            this.price = price;
            this.decision = decision;
            this.decisionDate = decisionDate;
            this.receiver = receiver;
            this.receiveDate = receiveDate;
            this.fabrnum = fabrnum;
        }
    }
}
