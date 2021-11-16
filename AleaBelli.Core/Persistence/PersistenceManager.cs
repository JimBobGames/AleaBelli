using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledCS;

namespace AleaBelli.Core.Persistence
{
    public class PersistenceManager
    {
        public TiledMap LoadTiledMap(string mapname)
        {
            var map = new TiledMap(mapname);
           // var tileset = new TiledTileset("path-to-tileset.tsx");

            return map;
        }
    }
}
