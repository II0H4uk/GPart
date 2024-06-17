using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class Benchmark
    {
        public static void RunAllAlgs(IAlg[] algs, int[][] graph, int k = 2, int startAlg = 0, int repeats = 1)
        {
            Console.WriteLine("Запуск всех методов...");

            for (int i = startAlg; i < algs.Length; i++)
                StartBenchmark(algs[i], graph, (i + 1).ToString(), k, repeats);

            Console.WriteLine("Конец работы всех методов\n");
        }

        private static void StartBenchmark(IAlg alg, int[][] graph, string name, int k = 2, int repeats = 1)
        {
            Console.WriteLine($"Результат работы {name} алгоритма:");

            TimeSpan totalTime = TimeComplexityAnalyzer.Calc_time(alg, graph, true, k, repeats);

            Console.WriteLine($"Время работы алгоритма: {totalTime}\n");
        }

        public static void CalcTimeComplexity()
        {
            Console.WriteLine("Старт рассчета вычислительной сложности...");

            double[][] nodeComp;
            double[][] edgeComp;

            int n = 20;

            IAlg[] algs = GetAllAlgs.Get2PartAlgs();

            for (int i = 0; i < algs.Length; i++)
            {
                nodeComp = TimeComplexityAnalyzer.ComputeComplexity_NodeCount(algs[i], n);  //для сокращения NCC
                Utilities.WriteFile(Utilities.WriteData(nodeComp), @"..\..\..\Report\NCC - " + n + " nodes alg" + (i + 1) + ".txt");

                edgeComp = TimeComplexityAnalyzer.ComputeComplexity_EdgeCount(algs[i], n * (n - 1) / 2, n);   //для сокращения ECC
                Utilities.WriteFile(Utilities.WriteData(edgeComp), @"..\..\..\Report\ECC - " + n + " nodes alg" + (i + 1) + ".txt");
            }

            Console.WriteLine("Complexity calculations complete\n");
        }

        public static void RunSingleAlg(IAlg alg, int[][] graph, string name, int k = 2, int repeats = 1)
        {
            Console.WriteLine("Запуск одного метода...");

            StartBenchmark(alg, graph, name, k, repeats);

            Console.WriteLine("Конец выполнения алгоритма\n");
        }
    }
}
