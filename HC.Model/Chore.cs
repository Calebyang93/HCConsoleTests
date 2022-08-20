using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model
{
   public class Chore
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private string ResourceCSV { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<string> Resources
        {
            get
            {
                return new List<string>(ResourceCSV.Split(','));
            }
        }

        public Chore(int id,string name, string resourceCSV, DateTime createdOn )
        {
            ID = id;
            Name = name;
            ResourceCSV = resourceCSV;
            CreatedOn = createdOn;
        }

        public override string ToString()
        {
            //return $"Chore {ID}: {Name} requires {ResourceCSV}";
            return $"Chore {ID}: {Name} {(string.IsNullOrEmpty(ResourceCSV) ? "(nothing needed)" : $"requires {ResourceCSV.Replace("\"",null)}")}";
        }
    }
}
