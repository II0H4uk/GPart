using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    interface IAlg
    {
        /// <summary>
        /// Старт алгоритма.
        /// </summary>
        /// <param name="graph">Матрица смежности.</param>
        /// <param name="k">Количество подграфов.</param>
        /// <returns>Разрез и значение критерия.</returns>
        (int[], int) Search(int[][] graph, int k = 2);
    }
}
