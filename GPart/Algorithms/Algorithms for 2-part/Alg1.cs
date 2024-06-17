using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Алгорим тривиального полного перебора
    /// </summary>
    public class Alg1 : AAlg
    {
        static protected double balance;

        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            Alg1 alg = new Alg1();
            init(graph, alg);
            
            alg.doSearch(alg.x, 0);
            return (alg.record_x, alg.record_cut);
        }

        static protected void init(int[][] graph, Alg1 alg)
        {
            balance = graph.Length % 2 == 1 ? 1.0 / graph.Length : 0;
            alg.init_base(graph);
        }

        private void doSearch(int[] x, int k)
        {
            if (k >= x.Length)
            {
                if (Evals.Balance(Graph, x) > balance) return;
                calc_cut(Graph, x);
                return;
            }
            x[k] = 0;
            doSearch(x, k + 1);
            x[k] = 1;
            doSearch(x, k + 1);
        }

        protected void calc_cut(int[][] graph, int[] x)
        {
            int cut = Evals.Cut(graph, x);
            save_record(x, cut);
        }
    }
}
