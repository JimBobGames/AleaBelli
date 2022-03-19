using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AleaBelli.Core.Data.Battle;

namespace AleaBelli.Core.Data
{
    /// <summary>
    /// There are two sides in a battle
    /// </summary>
    public class ScenarioSide : SimpleObject
    {
        private List<Army> armyList = new List<Army>();
        public List<Army> ArmyList
        {
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

    public class Scenario : SimpleObject
    {
        private List<ScenarioSide> sideList = new List<ScenarioSide>();

        public DateTime Date { get; set; }

        public Weather Weather { get; set; }

        public List<ScenarioSide> SideList { 
            get
            {
                return sideList;
            } 
            set
            {
                sideList = value;
            }
        }

        public ScenarioSide AddSide(ScenarioSide a)
        {
            sideList.Add(a);

            return a;
        }

    }
}
