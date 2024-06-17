using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// модифицирован алгоритм Катхилла-Макки
    /// вместо использования BFS, используется обход вершин с приоритетом по количеству 
    /// </summary>
    class Alg9 : Alg8
    {
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg9 alg = new Alg9();
            init(CuthillMcKee(graph), alg);

            alg.doSearch(alg.x, 0, 1, 0, 0);
            return (UnMap(alg.record_x), alg.record_cut);
        }

        protected override void BFS(int[][] graph, List<int> queue)
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
                    Dictionary<int, int> subQueue = new Dictionary<int, int>();

                    for (int i = 0; i < graph[curr].Length; i++)
                    {
                        if (!queue.Contains(graph[curr][i]))
                        {
                            subQueue.Add(graph[curr][i], graph[i].Length);
                            isChecked[graph[curr][i]] = true;
                        }
                    }
                    queue.AddRange(subQueue.OrderBy(val => val.Value).Select(x => x.Key).ToList());
                }

            } while (!isChecked.All(value => value));
        }
    }
}
