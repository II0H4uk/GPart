using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class PlanarGraphGenerator
    {
        /// <summary>
        /// Генерирует планарный граф.
        /// </summary>
        /// <param name="n">Количество вершин графа.</param>
        /// <param name="m">Количество граней графа.</param>
        /// <returns>Список смежности планарного графа.</returns>
        public static int[][] GenerateGraph(int n, int m = int.MaxValue)
        {
            int columns = (n > 15)? Rand.Next(3, (int)Math.Sqrt(n)) : 3;
            int rows = n / columns;
            List<int>[] graphList = new List<int>[n];
            for (int i = 0; i < n; i++)
                graphList[i] = new List<int>();

            int maxEdgeCount = n % columns == 0 ?
                3 * columns * rows - 2 * (columns + rows) + 1 :
                3 * columns * rows - 2 * (columns + rows) + 3 * (n % columns);

            m = m > maxEdgeCount ? maxEdgeCount : m;

            int currNode;
            int secondNode;
            
            while(m > 0)
            {
                currNode = Rand.Next(n - 1);
                secondNode = GetElem(currNode, Rand.Next(7), columns, n);

                if (secondNode == -1 || graphList[currNode].Contains(secondNode) || Collision(currNode, secondNode, columns, graphList))
                    continue;
                m--;
                graphList[currNode].Add(secondNode);
                graphList[secondNode].Add(currNode);
            }

            return graphList.Select(list => list.ToArray()).ToArray();
        }

        private static bool Collision(int firstNode, int secondNode, int columns, List<int>[] graphList)
        {
            if (firstNode / columns == secondNode / columns || firstNode % columns == secondNode % columns)
                return false;

            if (firstNode > secondNode)
                (firstNode, secondNode) = (secondNode, firstNode);

            int xOffset = (secondNode % columns) - (firstNode % columns);

            int otherFirst = firstNode + xOffset;
            int otherSecond = secondNode - xOffset;
            
            if (graphList[otherFirst].Contains(otherSecond))
                return true;
            return false;
        }

        private static int GetElem(int firstNodeNum, int secondNodePos, int columns, int n)
        {
            if (secondNodePos >= 4)
                secondNodePos++;

            int[] secondNodeOffset = new int[2];
            secondNodeOffset[0] = (secondNodePos % 3) - 1;
            secondNodeOffset[1] = (secondNodePos / 3) - 1;

            int[] secondNodeCoord = new int[2];
            secondNodeCoord[0] = (firstNodeNum % columns) + secondNodeOffset[0];
            secondNodeCoord[1] = (firstNodeNum / columns) + secondNodeOffset[1];

            if (secondNodeCoord[0] == columns || secondNodeCoord[0] < 0 || secondNodeCoord[1] < 0)
                return -1;

            int secondNodeNum = secondNodeCoord[1] * columns + secondNodeCoord[0];

            if (secondNodeNum < 0 || secondNodeNum >= n)
                return -1;
            return secondNodeNum;
        }
    }
}
