using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Model
{
    public class ChoreLog
    {
        public Guid ID { get; set; }
        public int ChoreID { get; set; }
        public DateTime DoneOn { get; set; }
        public string Note { get; set; }
        public string DoneBy { get; set; }

        public ChoreLog()
        {
            ID = Guid.NewGuid();
        }
    }
}
