using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AleaBelli.Core.Data.Battle;

namespace AleaBelli.Core.Data
{
    public class Scenario : SimpleObject
    {
        private List<Army> armyList = new List<Army>();

        public DateTime Date { get; set; }

        public Weather Weather { get; set; }

        public List<Army> ArmyList { 
            get
            {
                return armyList;
            } 
            set
            {
                armyList = value;
            }
        }

        public Army AddArmy(Army a)
        {
            armyList.Add(a);

            return a;
        }

    }
}
