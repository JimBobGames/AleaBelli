using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Util
{
    public class Rand
    {
        private static Random rnd = new Random();

        public static int RandD100()
        {
            return rnd.Next(1, 101);
        }

        public static int RandD12()
        {
            return rnd.Next(1, 13);
        }

        public static int RandD10()
        {
            return rnd.Next(1, 11);
        }

        public static int RandD6()
        {
            return rnd.Next(1, 7);
        }

        /// <summary>
        /// Random number between 1 and N
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int RandN(int n)
        {
            return rnd.Next(1, n + 1);
        }
    }
}
