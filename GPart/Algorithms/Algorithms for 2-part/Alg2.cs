using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// Добавлена упрощенная проверка на сбалансированность
    /// </summary>
    class Alg2 : Alg1
    {

        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg2 alg = new Alg2();
            init(graph, alg);

            alg.doSearch(alg.x, 0, 0, 0);
            return (alg.record_x, alg.record_cut);
        }

        private void doSearch(int[] x, int k, int n0, int n1)
        {
            if (n0 > mid_len || n1 > mid_len) return;
            if (k == x.Length)
            {
                if (Balance(n0, n1) > balance) return;
                calc_cut(Graph, x);
                return;
            }
            x[k] = 0;
            doSearch(x, k + 1, n0 + 1, n1);
            x[k] = 1;
            doSearch(x, k + 1, n0, n1 + 1);
        }

        private static double Balance(int n0, int n1)
        {
            return (double)Math.Abs(n0 - n1) / (n0 + n1);
        }
    }
}
