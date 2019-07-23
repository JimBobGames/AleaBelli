using AleaBelli.Core.Network;
using AleaBelli.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Persistence
{
    public class ReferenceDataPersistence : BasePersistence
    {
        public static void LoadNations(IAleaBelliGame g)
        {
            List<string> nations = ReadTextResource(g, "nations.txt");
            foreach (string s in nations)
            {
                // id,name
                string[] a = s.Split(',');
                int id = int.Parse(a[0].Trim());
                string name = a[1].Trim();
                string baseColor = a[2].Trim();
                Nation n = new Nation() { ShortName = name, NationId = id, BaseColour = baseColor };
                g.AddNation(n);
            }
        }
    }
}
