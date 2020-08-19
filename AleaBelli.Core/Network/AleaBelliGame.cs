using AleaBelli.Core.Data;
using AleaBelli.Core.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Network
{
    public class AleaBelliGame : IAleaBelliGame
    {
        private Dictionary<int, Player> players = new Dictionary<int, Player>();
        private Dictionary<int, Nation> nations = new Dictionary<int, Nation>();
        private Dictionary<int, Army> armies = new Dictionary<int, Army>();

        public Dictionary<int, Army> Armies
        {
            get
            {
                return armies;
            }
        }

        internal Player AddPlayer(Player p)
        {
            players[p.PlayerId] = p;

            return p;
        }

        internal Nation AddNation(Nation n)
        {
            nations[n.NationId] = n;

            return n;
        }

        internal Army AddArmy(Army a)
        {
            armies[a.ArmyId] = a;

            return a;
        }
    }
}
