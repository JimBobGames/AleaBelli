using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    /// <summary>
    /// The changes thats need to be redrawn
    /// </summary>
    public class UIChanges
    {
        private HashSet<int> regimentalIds = new HashSet<int>();

        public HashSet<int> RegimentalIds
        {
            get
            {
                return regimentalIds;
            }
        }
    }
}
