using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public class Corps : SimpleObject
    {
        private List<AleaBelli.Core.Data.ArmyDivision> divisions = new List<ArmyDivision>();

        public int CorpId { get; set; }
        public List<ArmyDivision> Divisons
        {
            get
            {
                return divisions;
            }
        }


        public ArmyDivision AddDivision(ArmyDivision d)
        {
            divisions.Add(d);

            return d;
        }

        public Officer Officer { get; set; }

    }
}
