using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class TimeComplexityAnalyzer
    {
        #region timer

        static long tick = 0;

        private static void start() => tick = DateTime.Now.Ticks;

        private static void stop() => tick = DateTime.Now.Ticks - tick;

        private static TimeSpan info() => new TimeSpan(tick);

        #endregion

        public static TimeSpan Calc_time(IAlg alg, int[][] graph, bool showRes = true, int k = 2, int repeats = 1)
        {
            //(int[] x, int cut) = alg.Search(matCopy, k);
            int[] x = new int[graph.Length];
            int cut = int.MaxValue;

            int countOptimAchieved = 0;

            start();
            for (int i = 0; i < repeats; i++)
            {
                (int[] currX, int currCut) = alg.Search(Utilities.CopyMat(graph), k);

                if (currCut < cut)
                {
                    cut = currCut;
                    Array.Copy(currX, x, graph.Length);
                    countOptimAchieved = 0;
                }

                if (currCut == cut)
                    countOptimAchieved++;
            }
            stop();

            if (repeats > 1)
            {
                Console.WriteLine($"Количество достижений наилучшего значения - {countOptimAchieved}");
                Console.WriteLine($"Процент достижения наилучшего значения - {(double)countOptimAchieved / repeats * 100}%");
            }

            if (showRes)
            {
                ShowResult.Cut(cut);
                ShowResult.X(x);
            }

            return info();
        }

        public static double[][] ComputeComplexity_NodeCount(IAlg alg, int MaxNodeCount)
        {
            double[][] result = new double[MaxNodeCount][];

            for (int i = 0; i < MaxNodeCount; i++)
            {
                int nodeCount = i + 2;
                int[][] graph = GraphGenerator.GenerateAdjListMinCon(nodeCount);
                //int[][] graph = GraphGenerator.GenerateAdjList(nodeCount, nodeCount * (nodeCount - 1) / 4);
                result[i] = new double[2];
                result[i][0] = nodeCount;
                result[i][1] = Calc_time(alg, graph, false).TotalMilliseconds;
            }

            return result;
        }

        public static double[] BenchmarkAlgsByRandomGraph(IAlg[] algs, int nodeCount, int cycleCount, int algCountStart = 0)
        {
            double[] result = new double[algs.Length];

            for (int k = 0; k < cycleCount; k++)
            {
                //int[][] graph = GraphGenerator.GenerateAdjList(nodeCount, nodeCount * (nodeCount - 1) / 4);
                int[][] graph = PlanarGraphGenerator.GenerateGraph(nodeCount);

                for (int i = algCountStart - 1; i < 8; i++)
                    result[i] += Calc_time(algs[i], graph, false).TotalMilliseconds;
                Console.WriteLine($"Прогресс: {(double)(k + 1) / cycleCount * 100}%");
            }

            return result.Select(x => x / (cycleCount * 1000)).ToArray();
        }

        public static double[][] ComputeComplexity_EdgeCount(IAlg alg, int maxEdgeCount, int nodeCount)
        {
            double[][] result = new double[maxEdgeCount][];

            for (int i = 0; i < maxEdgeCount; i++)
            {
                int[][] graph = GraphGenerator.GenerateAdjList(nodeCount, i + 1);
                result[i] = new double[2];
                result[i][0] = i + 1;
                result[i][1] = Calc_time(alg, graph, false).TotalMilliseconds;
                Console.WriteLine($"curr is {i + 1} of {maxEdgeCount}");
            }

            return result;
        }
    }
}
