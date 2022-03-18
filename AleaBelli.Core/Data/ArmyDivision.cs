using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public class ArmyDivision : SimpleObject
    {
        private List<Brigade> brigades = new List<Brigade>();

        public int DivisionId { get; set; }

        public List<Brigade> Brigades
        {
            get
            {
                return brigades;
            }
        }


        public Brigade AddBrigade(Brigade b)
        {
            brigades.Add(b);

            return b;
        }

        public Officer Officer { get; set; }
    }
}
