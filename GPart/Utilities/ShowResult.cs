using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class ShowResult
    {
        public static void Cut(int cut)
        {
            Console.WriteLine($"Значение критерия (разрез) = {cut}");
        }

        public static void X<T>(T[] x)
        {
            Console.Write("x = ");
            for (int i = 0; i < x.Length; i++)
            {
                Console.Write(x[i]);
                if (i != x.Length - 1)
                    Console.Write(", ");
            }
            Console.WriteLine();
        }

        public static void Graph(int[][] graph)
        {
            Console.WriteLine();
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                    Console.Write(graph[i][j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
