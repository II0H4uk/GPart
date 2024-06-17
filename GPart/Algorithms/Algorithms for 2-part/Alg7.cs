using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// Добавлена перенумерация вершин на основе алгоритма Катхилла-Макки
    /// </summary>
    class Alg7 : Alg6
    {
        static protected int[] diff;

        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg7 alg = new Alg7();
            init(CuthillMcKee(graph), alg);

            alg.doSearch(alg.x, 0, 1, 0, 0);
            return (UnMap(alg.record_x), alg.record_cut);
        }

        static protected void init(int[][] graph, Alg7 alg)
        {
            alg.init_base(graph);
        }

        protected int[][] CuthillMcKee(int[][] graph)
        {
            List<int> newSequence = new List<int>();
            newSequence.Add(0);
            BFS(graph, newSequence);

            diff = new int[newSequence.Count];

            for (int i = 0; i < newSequence.Count; i++)
                diff[i] = newSequence[i];
            return Mapping(graph);
        }

        protected static void BFS(int[][] graph, List<int> queue)
        {
            do
            {
                for (int j = 0; j < queue.Count; j++)
                {
                    int curr = queue[j];
                    for (int i = 0; i < graph[curr].Length; i++)
                        if (!queue.Contains(graph[curr][i]))
                            queue.Add(graph[curr][i]);
                }

            } while (!CheckCoherence(queue, graph.Length));
        }

        private static bool CheckCoherence(List<int> queue, int graphLen)
        {
            if (queue.Count == graphLen)
                return true;
            for (int i = 0; i < graphLen; i++)
            {
                if (!queue.Contains(i))
                {
                    queue.Add(i);
                    return false;
                }
            }
            return true;
        }

        protected int[][] Mapping(int[][] graph)
        {
            int[][] graphCopy = Utilities.CopyMat(graph);
            int[][] result = new int[graph.Length][];

            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                    graphCopy[i][j] = diff[graph[i][j]];

                Array.Sort(graphCopy[i]);

                result[diff[i]] = graphCopy[i];
            }

            return result;
        }

        protected int[] UnMap(int[] x) => Enumerable.Range(0, x.Length)
                      .Select(i => x[diff[i]])
                      .ToArray();
    }
}
