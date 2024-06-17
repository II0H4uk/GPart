using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class GraphGenerator
    {
        /// <summary>
        /// Генерирует веса граням графа.
        /// </summary>
        /// <param name="graph">Список смежности графа.</param>
        /// <returns>Матрица смежности взвешенного графа.</returns>
        public static int[][] GenerateWeightForkPart(int[][] graph)
        {
            int n = graph.Length;
            int[][] adjMat = Utilities.InitMat<int>(n, n);

            for (int i = 0; i < n; i++)
            {
                foreach (int neighbor in graph[i])
                {
                    if (adjMat[i][neighbor] == 0)
                    {
                        int weight = Rand.Next(1, 10);
                        (adjMat[i][neighbor], adjMat[neighbor][i]) = (weight, weight);
                    }
                }
            }

            return adjMat;
        }

        public static int[][] GenerateAdjMatrix(int n, int m = int.MaxValue)
        {
            int[][] graph = new int[n][];

            int maxEdgeCount = n * (n - 1) / 2;
            m = m > maxEdgeCount ? maxEdgeCount : m;

            for (int i = 0; i < n; i++)
                graph[i] = new int[n];

            int row, coll;
            for (int i = 0; i < m; i++)
            {
                row = Rand.Next(n);
                coll = Rand.Next(n);
                if (row != coll)
                {
                    graph[row][coll] = 1;
                    graph[coll][row] = 1;
                }
            }

            return graph;
        }

        /// <summary>
        /// Генерирует граф.
        /// </summary>
        /// <param name="n">Количество вершин графа.</param>
        /// <param name="m">Количество граней графа.</param>
        /// <returns>Список смежности графа.</returns>
        public static int[][] GenerateAdjList(int n, int m)
        {
            int queueLen = n * (n - 1) / 2;
            if (m > queueLen)
                m = queueLen;
            int[] queue = Enumerable.Range(0, queueLen).ToArray();

            for (int i = 0; i < queueLen - 1; i++)
            {
                int rndIndex = Rand.Next(i, queueLen - 1);
                (queue[i], queue[rndIndex]) = (queue[rndIndex], queue[i]);
            }

            List<List<int>> graph = new List<List<int>>();
            for (int i = 0; i < n; i++)
                graph.Add(new List<int>());

            for (int i = 0; i < m; i++)
            {
                int k = queue[i];
                int currLen = n - 1;
                int j = 0;
                while (k >= currLen)
                {
                    k -= currLen;
                    currLen--;
                    j++;
                }
                graph[j].Add(j + k + 1);
                graph[j + k + 1].Add(j);
            }

            ListListSort(graph);

            return graph.Select(l => l.ToArray()).ToArray();
        }

        //генерация списка смежности минимально связного графа
        public static int[][] GenerateAdjListMinCon(int nodes)
        {
            List<List<int>> adjList = ListInit(nodes);
            List<int> connected = new List<int>();
            connected.Add(Rand.Next(nodes));

            for (int i = 1; i < nodes; i++)
            {
                (int currEdge, int nextEdge) = GetNextEdges(nodes, connected);

                adjList[currEdge].Add(nextEdge);
                adjList[nextEdge].Add(currEdge);
            }

            ListListSort(adjList);

            return ConvertTypeListToMat(adjList);
        }

        private static void ListListSort(List<List<int>> adjList)
        {
            for (int i = 0; i < adjList.Count; i++)
                adjList[i].Sort();
        }

        private static int[][] ConvertTypeListToMat(List<List<int>> adjList)
        {
            int nodes = adjList.Count;
            int[][] matrix = new int[nodes][];

            for (int i = 0; i < nodes; i++)
                matrix[i] = adjList[i].ToArray();
            return matrix;
        }

        private static (int, int) GetNextEdges(int nodes, List<int> connected)
        {
            int edgeCurr = Rand.Next(0, connected.Count);
            int edgeNext;
            do
            {
                edgeNext = Rand.Next(0, nodes);
            } while (connected.Contains(edgeNext));

            connected.Add(edgeNext);

            return (connected[edgeCurr], edgeNext);
        }

        private static List<List<int>> ListInit(int nodes)
        {
            List<List<int>> adjList = new List<List<int>>();
            for (int i = 0; i < nodes; i++)
                adjList.Add(new List<int>());
            return adjList;
        }
    }
}
