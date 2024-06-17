using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    public abstract class AAlg : IAlg
    {
        private int[][] graph;
        protected int record_cut;
        protected int[] record_x;
        protected int mid_len;
        protected int[] x;

        protected int[][] Graph { get => graph; }

        protected void init_base(int[][] graph)
        {
            this.graph = graph;
            x = new int[graph.Length];
            record_cut = int.MaxValue;
            record_x = new int[graph.Length];
            mid_len = graph.Length / 2 + graph.Length % 2;
        }

        public abstract (int[], int) Search(int[][] graph, int k = 2);

        protected void save_record(int[] x, int cut)
        {
            if (record_cut > cut)
            {
                record_cut = cut;
                for (int i = 0; i < x.Length; i++) record_x[i] = x[i];
            }
        }
    }
}
