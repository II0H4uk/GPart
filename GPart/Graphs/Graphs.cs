using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    public static class Graphs
    {
        public static int[][] get_graph1()
        {
            return new int[5][]
            {
                    new int[] { 1, 2, 3},
                    new int[] { 0, 4},
                    new int[] { 0, 3},
                    new int[] { 0, 2, 4},
                    new int[] { 1, 3}
            };
        }

        public static int[][] get_graph2()
        {
            return new int[4][]
            {
                    new int[] { 1},
                    new int[] { 0, 2, 3},
                    new int[] { 1, 3},
                    new int[] { 1, 2}
            };
        }

        public static int[][] get_graph3()
        {
            return new int[9][]
            {
                    new int[] { 1, 8},
                    new int[] { 0, 2, 3, 4, 7},
                    new int[] { 1, 4},
                    new int[] { 1, 6},
                    new int[] { 1, 2, 5},
                    new int[] { 4, 6},
                    new int[] { 3, 5, 7},
                    new int[] { 1, 6, 8},
                    new int[] { 0, 7}
            };
        }

        public static int[][] get_graph3_optim()
        {
            return new int[9][]
            {
                    new int[] { 1, 2},
                    new int[] { 0, 3, 4, 5, 6},
                    new int[] { 0, 6},
                    new int[] { 1, 5},
                    new int[] { 1, 7},
                    new int[] { 1, 3, 8},
                    new int[] { 1, 2, 7},
                    new int[] { 4, 6, 8},
                    new int[] { 5, 7}
            };
        }

        public static int[][] get_graph3_incoherent()
        {
            return new int[9][]
            {
                    new int[] { 1, 8},
                    new int[] { 0, 2, 3, 4, 7},
                    new int[] { 1, 4},
                    new int[] { 1},
                    new int[] { 1, 2},
                    new int[] { 6},
                    new int[] { 5},
                    new int[] { 1, 8},
                    new int[] { 0, 7}
            };
        }

        public static int[][] get_graph_BFS_test()
        {
            return new int[9][]
            {
                    new int[] { 1, 4, 5},
                    new int[] { 0, 2, 5},
                    new int[] { 1, 3},
                    new int[] { 2},
                    new int[] { 0, 5},
                    new int[] { 0, 1, 4, 6},
                    new int[] { 5, 7},
                    new int[] { 6, 8},
                    new int[] { 7}
            };
        }
    }
}
