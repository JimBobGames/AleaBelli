using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    /// <summary>
    ///  Describes a gun or artillery piece
    /// </summary>
    public class Weapon : SimpleObject
    {
        public int WeaponId { get; set; }

        public int EffectiveRange { get; set; }

        public int MaximumRange { get; set; }
        public int Accuracy { get; set; }

        public int ShotsPerMinute { get; set; }
    }
}
