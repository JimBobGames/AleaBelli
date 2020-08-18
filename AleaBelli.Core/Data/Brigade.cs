using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public class Brigade : SimpleObject
    {
        private List<Regiment> regiments = new List<Regiment>();

        public int BrigadeId { get; set; }
        public List<Regiment> Regiments
        {
            get
            {
                return regiments;
            }
        }

    }
}
