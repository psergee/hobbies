using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitlesSimilarity.ML.Structures;

namespace TitlesSimilarity.ML.Algorithms
{
    class AgglomerativeClustering
    {
        struct Cooridnates
        {
            public LinkedListNode<Cluster> row;
            public LinkedListNode<Cluster> column;
        }

        /// <summary>
        /// Used for selection of similarity when mmerging two clusters. Can be re-assigned to use another algorithm.
        /// </summary>
        public Func<double, double, double> similaritySelector = Math.Min;

        /// <summary>
        /// Determines if found similarity/distance value should be processed. Default behavior is to ignore similarity below 0.6. 
        /// </summary>
        public Func<double, bool> shouldBeAnalyzed = val => val > .6;

        /// <summary>
        /// Performs non-hierachical clusterization
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="similarityMatrix">Matrix containing similarity.</param>
        /// <returns>Clusters containing data indexes.</returns>
        public IEnumerable<Cluster> Clusterize(double[][] similarityMatrix)
        {
            LinkedList<Cluster> activeClusters = new LinkedList<Cluster>();
            for (int i = 0; i < similarityMatrix.Length; ++i)
                activeClusters.AddLast(new Cluster(i));

            Cooridnates mostSimilar = FindMostSimilar(similarityMatrix, activeClusters);
            while (mostSimilar.row != null)
            {
                MergeClusters(similarityMatrix, activeClusters, mostSimilar);
                mostSimilar = FindMostSimilar(similarityMatrix, activeClusters);
            }

            return (from cluster in activeClusters select cluster);
        }

        /// <summary>
        /// Serahc similarity matrix for most similar data.
        /// </summary>
        /// <param name="similarityMatrix">Similarity matrix.</param>
        /// <param name="activeClusters">List of active clusters.</param>
        /// <returns>Most similar clusters.</returns>
        private Cooridnates FindMostSimilar(double[][] similarityMatrix, LinkedList<Cluster> activeClusters)
        {
            double maxValue = .0, currentValue;
            Cooridnates maxCoord = new Cooridnates();
            for (var iNode = activeClusters.First; iNode != null; iNode = iNode.Next)
            {
                for (var jNode = activeClusters.First; jNode != null &&
                jNode.Value.indexInMatrix < iNode.Value.indexInMatrix; jNode = jNode.Next)
                {
                    currentValue = similarityMatrix[iNode.Value.indexInMatrix][jNode.Value.indexInMatrix];
                    if (shouldBeAnalyzed(currentValue) && (currentValue > maxValue))
                    {
                        maxValue = currentValue;
                        maxCoord.row = iNode;
                        maxCoord.column = jNode;
                    }
                }
            }
            return maxCoord;
        }

        /// <summary>
        /// Merges clusters in one.
        /// </summary>
        /// <param name="similarityMatrix">Similarity matrix.</param>
        /// <param name="activeClusters">List of active clusters.</param>
        /// <param name="coordinates">Clusters to merge.</param>
        private void MergeClusters(double[][] similarityMatrix, LinkedList<Cluster> activeClusters, Cooridnates coordinates)
        {
            JaggedDistanceMatrix jm = new JaggedDistanceMatrix(similarityMatrix);
            int row = coordinates.row.Value.indexInMatrix, column = coordinates.column.Value.indexInMatrix;
            foreach (var cluster in activeClusters)
            {
                if (cluster.indexInMatrix == row || cluster.indexInMatrix == column)
                    continue;

                jm[cluster.indexInMatrix, row] = similaritySelector(jm[cluster.indexInMatrix, row], jm[cluster.indexInMatrix, column]);
            }
            coordinates.row.Value.items.AddRange(coordinates.column.Value.items);
            activeClusters.Remove(coordinates.column);
        }
    }
}
