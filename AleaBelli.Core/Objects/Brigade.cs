using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Objects
{
    public class Brigade : AbstractObject
    {
        private List<Battalion> battalions = new List<Battalion>();

        public int BrigadeId { get; set; }
        public List<Battalion> Battalions
        {
            get
            {
                return battalions;
            }
        }

    }
}
