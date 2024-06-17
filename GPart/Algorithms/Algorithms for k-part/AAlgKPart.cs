using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    /// <summary>
    /// алгоритм полного перебора k-разбиения
    /// </summary>
    abstract class AAlgKPart : IAlg
    {
        /// <summary>
        /// Список смежности графа.
        /// </summary>
        protected int[][] Graph { get; private set; }

        /// <summary>
        /// Матрица смежности графа.
        /// </summary>
        protected int[][] C { get; private set; }

        /// <summary>
        /// Количество вершин.
        /// </summary>
        protected int n;

        /// <summary>
        /// Количество подграфов.
        /// </summary>
        protected int k;

        /// <summary>
        /// Баланс.
        /// </summary>
        protected double balance;

        /// <summary>
        /// Текущий разрез.
        /// </summary>
        protected int[] x;

        /// <summary>
        /// Лучший разрез.
        /// </summary>
        protected int[] record_x;

        /// <summary>
        /// Лучшее значение критерия.
        /// </summary>
        protected int record_cut;

        public abstract (int[], int) Search(int[][] graph, int k = 2);
        
        static protected void Init(int[][] graph, int k, AAlgKPart alg)
        {
            alg.n = graph.Length;
            alg.k = k;

            alg.C = Utilities.CopyMat(graph);
            alg.Graph = ConvertGraph.AdjMatrixToList(alg.C);

            alg.balance = (double)alg.n / alg.k;

            alg.x = new int[alg.n];
            alg.record_x = new int[alg.n];
            alg.record_cut = int.MaxValue;
        }

        protected void CalcCrit()
        {
            int sum = 0;
            for (int i = 0; i < x.Length; i++)
                for (int j = i + 1; j < x.Length; j++)
                    if (Graph[i].Contains(j) && x[i] != x[j])
                        sum += C[i][j];

            if (sum < record_cut)
            {
                record_cut = sum;
                Array.Copy(x, record_x, x.Length);
            }
        }

        protected bool CheckBalance()
        {
            int[] nodeCount = new int[k];

            for (int i = 0; i < x.Length; i++)
                nodeCount[x[i]]++;

            for (int i = 0; i < k; i++)
                if (!(balance - 1 < nodeCount[i] && nodeCount[i] < balance + 1))
                    return false;

            return true;
        }
    }
}
