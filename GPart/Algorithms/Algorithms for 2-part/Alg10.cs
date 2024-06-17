using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// Метод ветвей и границ
    /// добавлено сравнение с результатом эвристического алгоритма по мере погружения
    /// </summary>
    class Alg10 : Alg8
    {
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            KernighanLin heuristicAlg = new KernighanLin(ConvertGraph.AdjListToMatrix(graph));
            heuristicAlg.PartitionGraph();
            int subOptimVal = Evals.Cut(graph, heuristicAlg.PrintPartitions());
            //Console.WriteLine($"KernighanLin x = {subOptimVal}");

            Alg10 alg = new Alg10();
            init(CuthillMcKee(graph), alg);

            alg.doSearch(alg.x, 0, 1, 0, 0, subOptimVal);
            return (UnMap(alg.record_x), alg.record_cut);
        }

        protected void doSearch(int[] x, int k, int n0, int n1, int cut, int subOptimVal)
        {
            if (cut > subOptimVal)
                return;

            for (int i = 0; i < Graph[k].Length && Graph[k][i] < k; i++)
                if (x[Graph[k][i]] != x[k])
                    cut++;

            if (cut > subOptimVal)
                return;

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
                doSearch(x, k + 1, n0 + 1, n1, cut, subOptimVal);
            }
            if (n1 < mid_len)
            {
                x[k + 1] = 1;
                doSearch(x, k + 1, n0, n1 + 1, cut, subOptimVal);
            }
        }
    }
}
