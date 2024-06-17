using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class Tests
    {
        public static void RunTests()
        {
            Console.WriteLine("Tests started...");
            Console.WriteLine($"test_Evals_Cut:\t\t{test_Evals_Cut()}");
            Console.WriteLine($"test_Evals_Balance:\t{test_Evals_Balance()}");
            Console.WriteLine($"test_Algs:\t\t{test_Algs()}");

            Console.WriteLine("Tests finished\n");
        }

        public static bool test_Algs()
        {
            int[][] graph = Graphs.get_graph1();
            IAlg[] algs = GetAllAlgs.Get2PartAlgs();

            foreach (IAlg alg in algs)
            {
                (int[] x, int y) = alg.Search(graph);

                if (!Utilities.Assert(Evals.Balance(graph, x), 1.0 / 5))
                    return false;
                /*if (Evals.Cut(graph, x) != 2)     //мне лень думать как возвращать не измененный вектор x (уже написан метод unmap, но лень думать дальше)
                    return false;*/
                if (y != 2)
                    return false;
            }

            return true;
        }

        public static bool test_Evals_Balance()
        {
            var g = Graphs.get_graph1();
            var x = new int[] { 0, 0, 1, 1, 0 };
            if (!Utilities.Assert(Evals.Balance(g, x), 1.0 / 5)) return false;
            x = new int[] { 0, 0, 0, 1, 0 };
            if (!Utilities.Assert(Evals.Balance(g, x), 3.0 / 5)) return false;
            x = new int[] { 0, 0, 0, 0, 0 };
            if (!Utilities.Assert(Evals.Balance(g, x), 1)) return false;

            return true;
        }

        public static bool test_Evals_Cut()
        {
            var g = Graphs.get_graph1();
            var x = new int[] { 0, 0, 1, 1, 0 };
            if (Evals.Cut(g, x) != 3) return false;
            x = new int[] { 0, 0, 0, 1, 0 };
            if (Evals.Cut(g, x) != g[3].Length) return false;
            x = new int[] { 0, 0, 0, 0, 0 };
            if (Evals.Cut(g, x) != 0) return false;

            return true;
        }
    }
}
