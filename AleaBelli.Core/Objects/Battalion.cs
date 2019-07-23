using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Objects
{
    public enum BattalionType
    {
        LineInfantry = 0,
        LightInfantry,
        LightCavalry,
        HeavyCavalry,
        HorseArtillery,
        Artillery
    }

    public enum BattalionFormation
    {
        Column = 0,
        Line1 = 1,
        Line2 = 2,
        Line3 = 3,
        Line4 = 4,
        Square
    }

    /// <summary>
    /// Not sure this type is needed was not a tactical unit, it was an administrative formation that never took the field. 
    /// </summary>
    public class Regiment : AbstractObject
    {

    }

    public class Battalion : AbstractObject
    {
        public int BattalionId { get; set; }
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int FacingInDegrees { get; set; }
        public BattalionFormation BattalionFormation { get; set; }
        public BattalionType BattalionType { get; set; }
        public int NationId { get; set; }
        public int Men { get; set; }
        public bool IsDirty { get; set; } // the regiments needs redrawing or perhaps a network update ?? 

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
            switch (BattalionType)
            {
                case BattalionType.LineInfantry: return GetLineInfantryWidth();
            }

            return 100;
        }

        public int GetDepthInPaces()
        {
            switch (BattalionType)
            {
                case BattalionType.LineInfantry: return GetLineInfantryDepth();
            }

            return 100;
        }

        private int GetLineInfantryWidth()
        {
            switch (BattalionFormation)
            {
                case BattalionFormation.Line1: return Men; // 1 rank
                case BattalionFormation.Line2: return Men / 2; // 2 rank
                case BattalionFormation.Line3: return Men / 3; // 3 rank
                case BattalionFormation.Line4: return Men / 4; // 4 rank
            }

            return 100;
        }

        private int GetLineInfantryDepth()
        {
            switch (BattalionFormation)
            {
                case BattalionFormation.Line1: return 10; // 1 rank
                case BattalionFormation.Line2: return 20; // 2 rank
                case BattalionFormation.Line3: return 30; // 3 rank
                case BattalionFormation.Line4: return 40; // 4 rank
            }

            return 100;
        }

        private int GetLightInfantryWidth()
        {
            throw new NotImplementedException();
        }

    }
}
