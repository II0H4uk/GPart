using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class KernighanLin
    {
        private int numVertices;
        private int[][] graph;
        private int[] partition;
        private int[] bestPartition;
        private bool[] locked;

        public KernighanLin(int[][] adjacencyMatrix)
        {
            numVertices = adjacencyMatrix.Length;
            graph = adjacencyMatrix;
            partition = new int[numVertices];
            bestPartition = new int[numVertices];
            locked = new bool[numVertices];
        }

        public void PartitionGraph()
        {
            Random random = new Random();

            for (int i = 0; i < numVertices; i++)
            {
                if (random.Next(2) == 0)
                    partition[i] = 1;
                else
                    partition[i] = 2;
            }

            int bestCutsize = ComputeCutsize(partition);
            Array.Copy(partition, bestPartition, numVertices);

            for (int iteration = 0; iteration < numVertices / 2; iteration++)
            {
                int maxGain = int.MinValue;
                int vertexToMove = -1;

                for (int i = 0; i < numVertices; i++)
                {
                    if (!locked[i])
                    {
                        int gain = ComputeGain(i);
                        if (gain > maxGain)
                        {
                            maxGain = gain;
                            vertexToMove = i;
                        }
                    }
                }

                if (vertexToMove == -1)
                    break;

                locked[vertexToMove] = true;
                MoveVertex(vertexToMove);

                int currentCutsize = ComputeCutsize(partition);
                if (currentCutsize < bestCutsize)
                {
                    bestCutsize = currentCutsize;
                    Array.Copy(partition, bestPartition, numVertices);
                }
            }

            Array.Copy(bestPartition, partition, numVertices);
        }

        public int[] PrintPartitions()
        {
            for (int i = 0; i < numVertices; i++)
            {
                partition[i]--;
            }
            return partition;
        }

        private int ComputeCutsize(int[] currentPartition)
        {
            int cutsize = 0;
            for (int i = 0; i < numVertices; i++)
            {
                for (int j = i + 1; j < numVertices; j++)
                {
                    if (currentPartition[i] != currentPartition[j] && graph[i][j] == 1)
                    {
                        cutsize++;
                    }
                }
            }
            return cutsize;
        }

        private int ComputeGain(int vertex)
        {
            int gain = 0;
            for (int i = 0; i < numVertices; i++)
            {
                if (!locked[i] && i != vertex)
                {
                    if (partition[i] == partition[vertex])
                    {
                        gain += graph[vertex][i];
                    }
                    else
                    {
                        gain -= graph[vertex][i];
                    }
                }
            }
            return gain;
        }

        private void MoveVertex(int vertex)
        {
            partition[vertex] = (partition[vertex] == 1) ? 2 : 1;
        }
    }
}
