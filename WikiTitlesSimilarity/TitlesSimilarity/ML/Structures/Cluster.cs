using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitlesSimilarity.ML.Structures
{
    class Cluster
    {
        public int indexInMatrix;
        public List<int> items;

        public Cluster(int index)
        {
            indexInMatrix = index;
            items = new List<int>(16);
            items.Add(index);
        }
    }
}
