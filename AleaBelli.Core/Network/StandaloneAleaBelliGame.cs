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
        /// Runs in the game UI Thread
        /// </summary>
        public new void UpdateGameVisuals()
        {

        }

        /// <summary>
        /// Runs in background thread CANNOT change visuals
        /// </summary>
        public void UpdateGameStates()
        {

        }

    }
}
