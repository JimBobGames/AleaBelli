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
        private Layout hexLayout;
        private HexMap map;
        private Dictionary<int, Player> players = new Dictionary<int, Player>();

        public void UpdateGameVisuals()
        {

        }

        internal void AddPlayer(Player p)
        {
            players[p.PlayerId] = p;
        }

        public Layout HexLayout
        {
            get
            {
                return hexLayout;
            }

            internal set
            {
                hexLayout = value;
            }

        }

        public HexMap HexMap
        {
            get
            {
                return map;
            }

            internal set
            {
                map = value;
            }

        }

        public bool RenderHexGrid
        {
            get;
            set;
        }
    }
}
