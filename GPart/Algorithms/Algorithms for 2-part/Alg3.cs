using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// добавлен рассчет сечения графа на основе ранее найденного
    /// </summary>
    class Alg3 : AAlg
    {
        static private int[] previous_x;
        static private int previous_cut;
        static private bool first_try;

        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg3 alg = new Alg3();
            init(graph, alg);

            alg.doSearch(alg.x, 0, 0, 0);
            return (alg.record_x, alg.record_cut);
        }

        static private void init(int[][] graph, Alg3 alg)
        {
            previous_x = new int[graph.Length];
            previous_cut = 0;
            first_try = true;
            alg.init_base(graph);
        }

        private void doSearch(int[] x, int k, int n0, int n1)
        {
            if (n0 > mid_len || n1 > mid_len) return;
            if (k == x.Length)
            {
                save_record(Graph, x);
                return;
            }
            x[k] = 0;
            doSearch(x, k + 1, n0 + 1, n1);
            x[k] = 1;
            doSearch(x, k + 1, n0, n1 + 1);
        }

        private void save_record(int[][] graph, int[] x)
        {
            int cut;
            if (first_try)
            {
                first_try = false;
                cut = Evals.Cut(graph, x);
            }
            else
            {
                int[] difference_x = find_difference(x);
                cut = Cut(graph, x, difference_x, previous_cut);
            }

            save_record(x, cut);

            previous_cut = cut;
            for (int i = 0; i < x.Length; i++)
                previous_x[i] = x[i];
        }

        private static int Cut(int[][] graph, int[] x, int[] difference_x, int previous_cut)
        {
            int res = previous_cut;

            for (int i = 0; i < difference_x.Length; i++)
            {
                int deg = graph[difference_x[i]].Length;
                for (int j = 0; j < deg; j++)
                {
                    if (difference_x.Contains(graph[difference_x[i]][j]))
                        continue;
                    res += x[difference_x[i]] != x[graph[difference_x[i]][j]] ? 1 : -1;
                }
            }
            return res;
        }

        private int[] find_difference(int[] x)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < x.Length; i++)
                if (x[i] != previous_x[i])
                    res.Add(i);
            return res.ToArray();
        }
    }
}
