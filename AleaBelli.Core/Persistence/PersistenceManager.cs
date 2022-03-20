using AleaBelli.Core.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void SaveScenario(Scenario sc, string filename)
        {
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, sc);
            }
        }

        public Scenario LoadScenario(string filename)
        {
            Scenario s;
            s = JsonConvert.DeserializeObject<Scenario>(File.ReadAllText(filename));
            return s;
        }
    }
}
