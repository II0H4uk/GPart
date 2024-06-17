using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class Utilities
    {
        private static double ZERO = 0.0000001;

        public static bool Assert(double a, double b)
        {
            return Math.Abs(a - b) < ZERO;
        }

        public static string WriteData<T>(T[][] graph)
        {
            string data = "";
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                    data += graph[i][j] + " ";
                data += "\n";
            }
            return data;
        }

        public static string WriteData<T>(T[] graph)
        {
            string data = "";
            for (int i = 0; i < graph.Length; i++)
                data += graph[i] + "\n";

            return data;
        }

        public static void WriteFile(string data, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(data);
            }
        }

        /// <summary>
        /// Создает копию матрицы.
        /// </summary>
        /// <param name="mat">Копируемая матрица.</param>
        /// <returns>Копию матрицы.</returns>
        public static T[][] CopyMat<T>(T[][] mat) => mat.Select(innerArray => innerArray.ToArray()).ToArray();

        public static T[][] InitMat<T>(int rows, int columns) => Enumerable.Range(0, rows).Select(_ => new T[columns]).ToArray();
    }
}
