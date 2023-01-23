using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starfire.Core.Data
{
    public class Counter
    {
        public int CounterId { get; set; }
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int FacingInDegrees { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
