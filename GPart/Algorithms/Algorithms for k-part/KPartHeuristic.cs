using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class KPartHeuristic : AAlgKPart
    {
        int[][] allDistances;
        int[] nodeCount;

        public override (int[], int) Search(int[][] graph, int k = 2)
        {
            KPartHeuristic alg = new KPartHeuristic();
            Init(graph, k, alg);

            alg.DoSearch();
            return (alg.record_x, alg.record_cut);
        }

        private void DoSearch()
        {
            HashSet<int> mainNodesHash = new HashSet<int>();
            while (mainNodesHash.Count < k)
                mainNodesHash.Add(Rand.Next(0, n));
            int[] mainNodes = mainNodesHash.ToArray();

            //int[] mainNodes = new int[] { 70, 92, 0, 15, 73 };

            GetAllDistances();

            GetMainNodesV2(mainNodes);

            //ShowResult.X(mainNodes);

            CalcSequenceV2(mainNodes);

            CalcCrit();
        }

        private void GetMainNodesV2(int[] mainNodes)
        {
            bool needMore = false;
            for (int i = 0; i < k; i++)
            {
                int maxScore = int.MinValue;
                int maxNode = -1;

                for (int j = 0; j < Graph[mainNodes[i]].Length; j++)
                {
                    int currScore = 0;
                    int currNode = Graph[mainNodes[i]][j];

                    if (mainNodes.Contains(currNode))
                        continue;

                    for (int p = 0; p < k; p++)
                    {
                        if (p == i)
                            continue;

                        currScore += allDistances[mainNodes[p]][currNode] - allDistances[mainNodes[p]][mainNodes[i]];
                    }

                    if (currScore > maxScore)
                        (maxScore, maxNode) = (currScore, currNode);

                    
                }
                if (maxScore > 0 || (maxScore == 0 && CheckDispersion(mainNodes[i], maxNode, mainNodes)))
                {
                    mainNodes[i] = maxNode;
                    //ShowResult.X(mainNodes);
                    i--;
                    needMore = true;
                }
            }
            if (needMore)
                GetMainNodesV2(mainNodes);
        }

        private bool CheckDispersion(int currMN, int nextMN, int[] mainNodes)
        {
            double currAvgSum = 0;
            double nextAvgSum = 0;

            double currDispersion = 0;
            double nextDispersion = 0;

            for (int i = 0; i < k; i++)
            {
                if (mainNodes[i] == currMN)
                    continue;
                currAvgSum += allDistances[currMN][mainNodes[i]];
                nextAvgSum += allDistances[nextMN][mainNodes[i]];
            }

            currAvgSum /= k - 1;
            nextAvgSum /= k - 1;

            for (int i = 0; i < k; i++)
            {
                if (mainNodes[i] == currMN)
                    continue;
                currDispersion += Math.Pow(allDistances[currMN][mainNodes[i]] - currAvgSum, 2);
                nextDispersion += Math.Pow(allDistances[nextMN][mainNodes[i]] - nextAvgSum, 2);
            }

            currDispersion = Math.Sqrt(currDispersion / 2);
            nextDispersion = Math.Sqrt(nextDispersion / 2);

            if (nextDispersion < currDispersion)
                return true;
            return false;
        }

        private void CalcSequenceV2(int[] mainNodes)
        {
            nodeCount = new int[k];
            nodeCount[0] = n;

            for (int i = 1; i < k; i++)
                for (int j = 0; j < n; j++)
                    if (CheckDist(mainNodes[i], j, mainNodes))
                    {
                        nodeCount[x[j]]--;
                        x[j] = i;
                        nodeCount[x[j]]++;
                    }

            RenumNodesByWeights(mainNodes);

            BalanceNodesV2();
        }

        private void BalanceNodesV2()
        {
            int nextSet = 0;

            while(CheckNodesCountV2(ref nextSet))
            {
                int[][] adjListForSets = GetAdjListForSets();

                int[] pathToSwap = GetPathToSwap(adjListForSets, nextSet);

                int nodeCountToSwap = (int)balance - nodeCount[nextSet];

                SwapNodes(pathToSwap, nodeCountToSwap);
            }
        }

        private void SwapNodes(int[] pathToSwap, int nodeCountToSwap)
        {
            List<int>[] nodesInSets = new List<int>[k];
            for (int i = 0; i < k; i++)
                nodesInSets[i] = new List<int>();

            for (int i = 0; i < n; i++)
                nodesInSets[x[i]].Add(i);

            for (int k = pathToSwap.Length - 2; k >= 0; k--)    //перемещаем вершины между подграфами
            {
                List<int[]> nodesToSwap = new List<int[]>();
                int currSet = pathToSwap[k];
                for (int i = 0; i < nodesInSets[currSet].Count; i++)    //выбираем текущую вершину
                {
                    int currNode = nodesInSets[currSet][i];
                    for (int j = 0; j < Graph[currNode].Length; j++)    //выбираем смежную к текущей вершине
                    {
                        int currAdjNode = Graph[currNode][j];

                        if (x[currNode] != currSet || x[currAdjNode] != pathToSwap[k + 1])
                            continue;

                        int sum = CalcCost(currNode, currAdjNode);

                        nodesToSwap.Add(new int[] {sum, currNode, currAdjNode});
                    }
                }

                nodesToSwap.Sort((x, y) => x[0].CompareTo(y[0]));

                TransferNode(nodesToSwap[0][1], nodesToSwap[0][2]);

                /*RemoveNonProfitable(nodesToSwitch);

                for (int i = 0; i < nodeCountToSwap; i++)   //передаем вершины между подграфами
                {
                    TransferNode(nodesToSwitch)
                }*/
            }
            
        }

        private void RemoveNonProfitable(List<int[]> nodesToSwitch)
        {

        }

        private int CalcCost(int currNode, int currAdjNode)
        {
            int sum = 0;

            for (int p = 0; p < Graph[currAdjNode].Length; p++)
                if (x[Graph[currAdjNode][p]] == x[currAdjNode])
                    sum += C[Graph[currAdjNode][p]][currAdjNode];
            sum -= C[currNode][currAdjNode];
            return sum;
        }

        private int[] GetPathToSwap(int[][] adjListForSets, int startSet, bool noOverflowSets = false)
        {
            List<int> shortestPath = new List<int>();
            Dictionary<int, int> prev = new Dictionary<int, int>();
            bool[] visited = new bool[k];
            Queue<int> queue = new Queue<int>();

            visited[startSet] = true;
            queue.Enqueue(startSet);

            while (queue.Count != 0)
            {
                int currSet = queue.Dequeue();

                if (nodeCount[currSet] > balance + 0.99 || (noOverflowSets && nodeCount[currSet] > balance))   // восстановление кратчайшего пути
                {
                    int backtrackSet = currSet;
                    while (backtrackSet != startSet)
                    {
                        shortestPath.Insert(0, backtrackSet);
                        backtrackSet = prev[backtrackSet];
                    }
                    shortestPath.Insert(0, startSet);
                    break;
                }

                for (int i = 0; i < adjListForSets[currSet].Length; i++)
                {
                    int adjSet = adjListForSets[currSet][i];
                    if (visited[adjSet])
                        continue;

                    visited[adjSet] = true;
                    prev[adjSet] = currSet;
                    queue.Enqueue(adjSet);
                }
            }

            if (shortestPath.Count == 0)
                return GetPathToSwap(adjListForSets, startSet, true);
            return shortestPath.ToArray();
        }

        private bool CheckNodesCountV2(ref int nextSet)
        {
            for (int i = 0; i < k; i++)
            {
                if (nodeCount[i] < balance - 0.99)
                {
                    nextSet = i;
                    return true;
                }
            }

            for (int i = 0; i < k; i++)
            {
                if (nodeCount[i] > balance + 0.99)
                {
                    for (int j = 0; j < k; j++)
                        if (nodeCount[j] > balance - 0.99 && nodeCount[j] < balance + 0.01)
                        {
                            nextSet = j;
                            return true;
                        }
                }
            }
            return false;
        }

        private int[][] GetAdjListForSets()
        {
            bool[] visited = new bool[n];

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(0);
            visited[0] = true;

            List<int>[] adjListForSets = new List<int>[k];
            for (int i = 0; i < k; i++)
                adjListForSets[i] = new List<int>();

            while(queue.Count != 0)
            {
                int currNode = queue.Dequeue();

                for (int i = 0; i < Graph[currNode].Length; i++)
                {
                    int adjNode = Graph[currNode][i];

                    if (!(x[currNode] == x[adjNode]) && !adjListForSets[x[currNode]].Contains(x[adjNode]))
                    {
                        adjListForSets[x[currNode]].Add(x[adjNode]);
                        adjListForSets[x[adjNode]].Add(x[currNode]);
                    }

                    if (visited[adjNode])
                        continue;

                    queue.Enqueue(adjNode);
                    visited[adjNode] = true;
                }
            }

            return adjListForSets.Select(list => list.ToArray()).ToArray();
        }

        private void TransferNode(int currNode, int currAdjNode)
        {
            nodeCount[x[currAdjNode]]--;
            x[currAdjNode] = x[currNode];
            nodeCount[x[currAdjNode]]++;
        }

        
        private void RenumNodesByWeights(int[] mainNodes)
        {
            for (int i = 0; i < n; i++)
                    RenumCurrNode(i, mainNodes, i);
        }

        private void RenumCurrNode(int currNode, int[] mainNodes, int globalCurrNode)
        {
            List<int> closeSets = new List<int>();
            closeSets.Add(x[currNode]);

            for (int i = 0; i < Graph[currNode].Length; i++)
                if (!closeSets.Contains(x[Graph[currNode][i]]))
                    closeSets.Add(x[Graph[currNode][i]]);

            int[] cost = new int[closeSets.Count];

            for (int i = 0; i < closeSets.Count; i++)
                for (int j = 0; j < Graph[currNode].Length; j++)
                {
                    int currLinkedNode = Graph[currNode][j];
                    if (x[currLinkedNode] == closeSets[i])
                        cost[i] += C[currNode][currLinkedNode];
                }

            if (x[currNode] == closeSets[Array.IndexOf(cost, cost.Max())] || nodeCount[x[currNode]] == 1 /*mainNodes.Contains(currNode)*/)
                return;

            nodeCount[x[currNode]]--;
            x[currNode] = closeSets[Array.IndexOf(cost, cost.Max())];
            nodeCount[x[currNode]]++;

            for (int i = 0; i < Graph[currNode].Length; i++)
            {
                if (Graph[currNode][i] > globalCurrNode)
                    break;
                RenumCurrNode(Graph[currNode][i], mainNodes, globalCurrNode);

            }

        }

        private bool CheckDist(int origin, int end, int[] mainNodes)
        {
            int newOriginDist = allDistances[origin][end];
            int oldOriginDist = allDistances[mainNodes[x[end]]][end];

            if (newOriginDist <= oldOriginDist)
                return true;

            return false;
        }

        private void GetAllDistances()
        {
            allDistances = new int[n][];

            for (int i = 0; i < n; i++)
                allDistances[i] = GetAllDistFromOrigin(i);
        }

        private int[] GetAllDistFromOrigin(int origin)
        {
            bool[] visited = new bool[n];
            int[] dist = new int[n];

            Queue<int> queue = new Queue<int>();
            visited[origin] = true;
            queue.Enqueue(origin);

            while (queue.Count != 0)
            {
                int currOrigin = queue.Dequeue();
                for (int i = 0; i < Graph[currOrigin].Length; i++)
                {
                    int currNode = Graph[currOrigin][i];
                    if (!visited[currNode])
                    {
                        queue.Enqueue(currNode);
                        visited[currNode] = true;
                        dist[currNode] = dist[currOrigin] + 1;
                    }
                }
            }

            return dist;
        }
    }
}
