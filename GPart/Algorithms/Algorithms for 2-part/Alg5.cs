using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// Добавлен рассчет сечения на основе погружения
    /// </summary>
    class Alg5 : AAlg
    {
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg5 alg = new Alg5();
            init(graph, alg);

            alg.doSearch(alg.x, 1, 1, 0, 0);
            return (alg.record_x, alg.record_cut);
        }

        static private void init(int[][] graph, Alg5 alg)
        {
            alg.init_base(graph);
        }

        private void doSearch(int[] x, int k, int n0, int n1, int cut)
        {
            if (x[k - 1] == 1)
                for (int i = 0; i < Graph[k - 1].Length && k > Graph[k - 1][i]; i++) cut += 1 - x[Graph[k - 1][i]];
            else
                for (int i = 0; i < Graph[k - 1].Length && k > Graph[k - 1][i]; i++) cut += x[Graph[k - 1][i]];

            if (k == x.Length)
            {
                save_record(x, cut);
                return;
            }
            if (n0 < mid_len)
            {
                x[k] = 0;
                doSearch(x, k + 1, n0 + 1, n1, cut);
            }
            if (n1 < mid_len)
            {
                x[k] = 1;
                doSearch(x, k + 1, n0, n1 + 1, cut);
            }
            x[k] = 0;
        }
    }
}
