using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechPoznanNaprawy.Database
{
    [Serializable]
    public class Settings
    {
        public string lastID { get; set; }
        public DateTime lastBackup { get; set; }

        public List<String> brands = new List<string>();

        public Settings() { }

        public Settings(string id, DateTime backup, List<string> brandslist)
        {
            lastID = id;
            lastBackup = backup;
            brands = brandslist;
        }
    }
}
