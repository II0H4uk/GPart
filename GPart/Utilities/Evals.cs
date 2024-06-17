using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    public static class Evals
    {
        public static int Cut(int[][] graph, int[] x)
        {
            int res = 0;
            int n = graph.Length;
            for (int i = 0; i < n; i++)
            {
                int deg = graph[i].Length;
                for (int j = 0; j < deg; j++)
                {
                    res += x[i] * (1 - x[graph[i][j]]);
                }
            }
            return res;
        }

        public static double Balance(int[][] graph, int[] x)
        {
            int res = 0;
            int n = graph.Length;
            for (int i = 0; i < n; i++)
            {
                res += x[i];
            }
            return (double)Math.Abs(res - (n - res)) / n;
        }
    }
}
