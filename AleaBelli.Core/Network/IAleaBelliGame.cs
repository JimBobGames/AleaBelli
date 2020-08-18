using AleaBelli.Core.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Network
{
    public interface IAleaBelliGame
    {
        bool RenderHexGrid
        {
            get;
            set;
        }

        HexMap HexMap { get; }
        Layout HexLayout { get; }

        void UpdateGameVisuals();
    }
}
