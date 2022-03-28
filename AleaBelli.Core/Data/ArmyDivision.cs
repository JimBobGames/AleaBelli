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

        public int Men
        {
            get
            {
                int men = 0;
                foreach (Brigade b in brigades)
                {
                    men += b.Men;
                }
                return men;
            }
            set
            {

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
