using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class KPartBruteForce : AAlgKPart
    {
        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            KPartBruteForce alg = new KPartBruteForce();
            Init(graph, k, alg);

            alg.DoSearch(0, alg.k);
            return (alg.record_x, alg.record_cut);
        }

        protected void DoSearch(int currIndex, int maxVal)
        {
            if (CheckBalance())
                CalcCrit();

            for (int i = currIndex; i < x.Length; i++)
            {
                if (x[i] + 1 >= maxVal)
                    continue;
                x[i]++;
                DoSearch(i, maxVal);
            }
            x[currIndex]--;
        }
    }
}
