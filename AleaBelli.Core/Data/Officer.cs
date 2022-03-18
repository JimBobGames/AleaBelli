using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Data
{
    public enum OfficerRank { MajorGeneral, BrigadierGeneral, Colonel, LieutenantColonel, Major, Captain, Lieutenant };

    public class Officer : SimpleObject
    {
        public int OfficereId { get; set; }

        public OfficerRank OfficerRank { get; set; }

    }
}
