using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public enum RegimentType
    {
        LineInfantry = 0,
        LightInfantry,
        LightCavalry,
        HeavyCavalry,
        HorseArtillery,
        Artillery
    }

    public enum RegimentFormation
    {
        Column = 0,
        Line1 = 1,
        Line2 = 2,
        Line3 = 3,
        Line4 = 4,
        Square
    }


    public class Regiment : SimpleObject
    {
        public int RegimentId { get; set; }
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int FacingInDegrees { get; set; }
        public RegimentFormation RegimentFormation { get; set; }
        public RegimentType RegimentType { get; set; }
        public int NationId { get; set; }
        public int Men { get; set; }
        public bool IsDirty { get; set; } // the regiments needs redrawing or perhaps a network update ?? 
        public List<MovementOrders> MovementOrders { get; set; }

        public int CurrentWidth
        {
            get
            {
                return 160;
            }
        }

        public int CurrentHeight
        {
            get
            {
                return 100;
            }
        }

        public int GetWidthInPaces()
        {
            return 100;
        }

        public int GetDepthInPaces()
        {
            return 15;
        }

        /*
        public int GetWidthInPaces()
        {
            switch (RegimentType)
            {
                case RegimentType.LineInfantry: return GetLineInfantryWidth();
            }

            return 100;
        }

        public int GetDepthInPaces()
        {
            switch (RegimentType)
            {
                case RegimentType.LineInfantry: return GetLineInfantryDepth();
            }

            return 100;
        }
        */
        private int GetLineInfantryWidth()
        {
            switch (RegimentFormation)
            {
                case RegimentFormation.Line1: return Men; // 1 rank
                case RegimentFormation.Line2: return Men / 2; // 2 rank
                case RegimentFormation.Line3: return Men / 3; // 3 rank
                case RegimentFormation.Line4: return Men / 4; // 4 rank
            }

            return 100;
        }

        private int GetLineInfantryDepth()
        {
            switch (RegimentFormation)
            {
                case RegimentFormation.Line1: return 10; // 1 rank
                case RegimentFormation.Line2: return 20; // 2 rank
                case RegimentFormation.Line3: return 30; // 3 rank
                case RegimentFormation.Line4: return 40; // 4 rank
            }

            return 100;
        }

        private int GetLightInfantryWidth()
        {
            throw new NotImplementedException();
        }

    }
}
