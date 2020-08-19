using AleaBelli.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Network
{
    public class StandaloneAleaBelliGame : AleaBelliGame
    {
        /// <summary>
        /// Runs in background thread CANNOT change visuals
        /// </summary>
        public void UpdateGameStates()
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
        }

    }
}
