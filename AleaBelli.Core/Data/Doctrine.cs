using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public abstract class Doctrine : SimpleObject
    {
        public int DoctrineId { get; set; }
    }

    /// <summary>
    /// Advance to effective range fire one volley charge
    /// </summary>
    public class AdvanceVolleyCharge : Doctrine
    {

    }
    /// <summary>
    /// Advance to effective range fire multiple volleys
    /// </summary>
    public class AdvanceExchangeCharge : Doctrine
    {

    }
}
