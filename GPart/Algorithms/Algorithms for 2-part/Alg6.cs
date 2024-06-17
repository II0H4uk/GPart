using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// Модифицирован рассчет сечения на основе погружения
    /// </summary>
    class Alg6 : AAlg
    {
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg6 alg = new Alg6();
            init(graph, alg);

            alg.doSearch(alg.x, 0, 1, 0, 0);
            return (alg.record_x, alg.record_cut);
        }

        static private void init(int[][] graph, Alg6 alg)
        {
            alg.init_base(graph);
        }

        protected void doSearch(int[] x, int k, int n0, int n1, int cut)
        {
            for (int i = 0; i < Graph[k].Length && Graph[k][i] < k; i++)
                if (x[Graph[k][i]] != x[k])
                    cut++;

            if (k + 1 == x.Length)
            {
                save_record(x, cut);
                return;
            }

            if (cut >= record_cut)
                return;

            if (n0 < mid_len)
            {
                x[k + 1] = 0;
                doSearch(x, k + 1, n0 + 1, n1, cut);
            }
            if (n1 < mid_len)
            {
                x[k + 1] = 1;
                doSearch(x, k + 1, n0, n1 + 1, cut);
            }
        }
    }
}
