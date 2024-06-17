using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class GraphRead
    {
        public static int[][] AdjMatFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            int[][] matrix = new int[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] elements = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> row = new List<int>();

                for (int j = 0; j < elements.Length; j++)
                    row.Add(int.Parse(elements[j]));
                matrix[i] = row.ToArray();
            }

            return matrix;
        }

        public static int[][] AdjListFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            int[][] matrix = new int[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] elements = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> row = new List<int>();

                for (int j = 0; j < elements.Length; j++)
                    if (int.Parse(elements[j]) != 0)
                        row.Add(j);
                matrix[i] = row.ToArray();
            }

            return matrix;
        }
    }
}
