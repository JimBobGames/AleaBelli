using AleaBelli.Core.Data;
using AleaBelli.Core.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Network
{
    public interface IAleaBelliGame
    {
        Dictionary<int, Army> Armies
        {
            get;
        }
    }
}
