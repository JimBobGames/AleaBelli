using Starfire.Core.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starfire.UI
{
    public class MapHelper
    {
        public static HexMap LoadMap(string filename)
        {
            int height = 4, width = 4;

            // create the game map
            HexMap hexMap = HexMap.CreateMap(height, width);

            return hexMap;
        }
    }
}

