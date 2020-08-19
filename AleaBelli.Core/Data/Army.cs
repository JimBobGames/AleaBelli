using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public class Army : SimpleObject
    {
        private List<Corps> corps = new List<Corps>();

        public int ArmyId { get; set; }
        public List<Corps> Corps
        {
            get
            {
                return corps;
            }
        }

        public Corps AddCorps(Corps c)
        {
            corps.Add(c);

            return c;
        }


    }
}
