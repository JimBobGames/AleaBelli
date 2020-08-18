using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace AleaBelli.Core.Persistence
{
    public class PersistenceManager
    {
        public TmxMap LoadTiledMap(string mapname)
        {
            var map = new TmxMap(mapname);

            return map;
        }
    }
}
