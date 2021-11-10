using AleaBelli.Core.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public enum BrigadeFormation
    {
        Column = 0,
        SingleLine = 1,
        DoubleLine = 2,
    }

    public class Brigade : SimpleObject
    {
        private List<Regiment> regiments = new List<Regiment>();
        public int BrigadeId { get; set; }
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int FacingInDegrees { get; set; }
        public BrigadeFormation BrigadeFormation { get; set; }
        public Officer Officer { get; set; }
        public List<MovementOrders> MovementOrders { get; set; }

        public List<Regiment> Regiments
        {
            get
            {
                return regiments;
            }
        }

        public Regiment AddRegiment(Regiment r)
        {
            regiments.Add(r);

            return r;
        }

        public int GetWidthInPaces()
        {
            return 15;
        }

        public int GetDepthInPaces()
        {
            return 15;
        }


    }
}
