using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// ускорен алгоритм Катхилла-Макки
    /// стартовая вершина является вершиной с наибольшим количеством граней
    /// </summary>
    class Alg8 : Alg7
    {
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg8 alg = new Alg8();
            init(CuthillMcKee(graph), alg);

            alg.doSearch(alg.x, 0, 1, 0, 0);
            return (alg.record_x, alg.record_cut);
        }

        protected new virtual int[][] CuthillMcKee(int[][] graph)
        {
            List<int> newSequence = new List<int>();
            BFS(graph, newSequence);

            diff = new int[newSequence.Count];
            for (int i = 0; i < newSequence.Count; i++)
                diff[i] = newSequence[i];
            return Mapping(graph);
        }

        protected new virtual void BFS(int[][] graph, List<int> queue)
        {
            bool[] isChecked = Enumerable.Repeat(false, graph.Length).ToArray();

            do
            {
                int currStartNode = -1;

                for (int i = 0, max = -1; i < graph.Length; i++)
                    if (graph[i].Length > max && !isChecked[i])
                        (currStartNode, max) = (i, graph[i].Length);

                queue.Add(currStartNode);
                isChecked[currStartNode] = true;

                for (int j = 0; j < queue.Count; j++)
                {
                    int curr = queue[j];
                    for (int i = 0; i < graph[curr].Length; i++)
                        if (!queue.Contains(graph[curr][i]))
                        {
                            queue.Add(graph[curr][i]);
                            isChecked[graph[curr][i]] = true;
                        }
                }

            } while (!isChecked.All(value => value));
        }
    }
}
