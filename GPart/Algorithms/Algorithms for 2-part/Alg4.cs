using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// модифицирована идея рассчета критерия на основе предыдущего
    /// умеьшен порядок перебора на 1 степень
    /// </summary>
    class Alg4 : AAlg
    {
        static private int[] previous_x;
        static private int previous_cut;
                        
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg4 alg = new Alg4();
            init(graph, alg);

            alg.doSearch(alg.x, 1, 1, 0);
            return (alg.record_x, alg.record_cut);
        }

        static private void init(int[][] graph, Alg4 alg)
        {
            previous_x = new int[graph.Length];
            previous_cut = 0;
            alg.init_base(graph);
        }

        void doSearch(int[] x, int k, int n0, int n1)
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
            int cut = Cut(graph, x, previous_x, previous_cut);

            save_record(x, cut);

            previous_cut = cut;
            x.CopyTo(previous_x, 0);
        }

        public static int Cut(int[][] graph, int[] x, int[] previous_x, int previous_cut)
        {
            if (previous_cut == int.MaxValue)
                return Evals.Cut(graph, x);

            int res = previous_cut;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == previous_x[i])
                    continue;
                int deg = graph[i].Length;
                for (int j = 0; j < deg; j++)
                {
                    if (x[graph[i][j]] != previous_x[graph[i][j]])
                        continue;
                    res += x[i] != x[graph[i][j]] ? 1 : -1;
                }
            }
            return res;
        }
    }
}
