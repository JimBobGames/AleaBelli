using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Objects
{
    public class Division : AbstractObject
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

    }
}
