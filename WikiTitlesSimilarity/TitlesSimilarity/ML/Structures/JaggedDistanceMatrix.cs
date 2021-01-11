using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitlesSimilarity.ML.Metrics;

namespace TitlesSimilarity.ML.Structures
{
    class JaggedDistanceMatrix
    {
        private double[][] matrix;

        public JaggedDistanceMatrix(double[][] originalMatrix)
        {
            matrix = originalMatrix;
        }

        public double this[int i, int j]
        {
            get
            {
                int row = Math.Max(i, j);
                int column = row == i ? j : i;
                return matrix[row][column];
            }

            set
            {
                int row = Math.Max(i, j);
                int column = row == i ? j : i;
                matrix[row][column] = value;
            }
        }

        /// <summary>
        /// Produces similarity matrix.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="dataArray">Elements to compute similarity.</param>
        /// <param name="similarityComputer">Used to compute similarity between two objects.</param>
        /// <returns></returns>
        public static double[][] ComputeSimilarity<T>(T[] dataArray, ISimilarity<T> similarityComputer)
        {
            double[][] similarityMatrix = new double[dataArray.Length][];
            for (int i = dataArray.Length - 1; i >= 0; --i)
            {
                similarityMatrix[i] = new double[i + 1];
                similarityMatrix[i][i] = 1;
                for (int j = 0; j < i; ++j)
                    similarityMatrix[i][j] = similarityComputer.Similarity(dataArray[i], dataArray[j]);
            }

            return similarityMatrix;
        }
    }
}
