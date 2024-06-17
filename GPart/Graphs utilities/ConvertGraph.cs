using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class ConvertGraph
    {
        public static int[][] AdjListToMatrix(int[][] graph)
        {
            int[][] matrix = new int[graph.Length][];
            for (int i = 0; i < graph.Length; i++)
            {
                matrix[i] = new int[graph.Length];
                for (int j = 0; j < graph[i].Length; j++)
                    matrix[i][graph[i][j]] = 1;
            }
            return matrix;
        }

        public static int[][] AdjMatrixToList(int[][] graph)
        {
            int n = graph.Length;
            int[][] res = new int[n][];
            for (int i = 0; i < n; i++)
            {
                List<int> adj = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    if (graph[i][j] != 0) adj.Add(j);
                }
                res[i] = adj.ToArray();
            }
            return res;
        }
    }
}
