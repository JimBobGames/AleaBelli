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
        private Dictionary<int, Regiment> regiments = new Dictionary<int, Regiment>();

        /// <summary>
        /// Runs in background thread CANNOT change visuals
        /// </summary>
        public void UpdateGameStates(UIChanges changes)
        {
            // update the regiments if required
            foreach (Army a in Armies.Values)
            {
                foreach (Corps c in a.Corps)
                {
                    foreach (ArmyDivision d in c.Divisons)
                    {

                        foreach (Brigade b in d.Brigades)
                        {
                            foreach (Regiment r in b.Regiments)
                            {
                                r.FacingInDegrees += 1;
                            }
                        }
                    }

                }
            }

            //// Hack 1 regiment changes
            changes.RegimentalIds.Add(1);
        }


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

        internal Regiment AddRegiment(Brigade b, Regiment r)
        {
            b.AddRegiment(r);
            regiments[r.RegimentId] = r;
            return r;
        }

        public Regiment GetRegiment(int regimentId)
        {
            Regiment r;
            regiments.TryGetValue(regimentId, out r);
            return r;
        }

    }
}
