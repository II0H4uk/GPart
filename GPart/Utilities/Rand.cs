using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    public static class Rand
    {
        static Random rnd = new Random();

        public static int Next(int max) => rnd.Next(max);
        public static int Next(int min, int max) => rnd.Next(min, max);
    }
}
