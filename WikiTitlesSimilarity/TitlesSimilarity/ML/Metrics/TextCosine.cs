using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TitlesSimilarity.ML.Metrics
{
    /// <summary>
    ///   Cosine similarity algorithm for texts.
    /// </summary>
    public class TextCosine : ISimilarity<string>
    {
        /// <summary>
        ///   Computes a similarity measure between two sentences.
        /// </summary>
        ///
        /// <param name="a">The first text to be compared.</param>
        /// <param name="b">The second text to be compared.</param>
        /// 
        /// <returns>Similarity between a and b.</returns>
        /// 
        public double Similarity(string a, string b)
        {
            var aTerms = GetTermsOccurrence(a);
            var bTerms = GetTermsOccurrence(b);

            var commonWords = aTerms.Keys.Intersect(bTerms.Keys);
            double dotProduct = commonWords.Aggregate(0.0, (product, word) => product += aTerms[word] * bTerms[word]);
            double aMagnitude = aTerms.Aggregate(0.0, (magnitude, wordOccurence) => magnitude += wordOccurence.Value * wordOccurence.Value);
            double bMagnitude = bTerms.Aggregate(0.0, (magnitude, wordOccurence) => magnitude += wordOccurence.Value * wordOccurence.Value);

            return dotProduct / (Math.Sqrt(aMagnitude) * Math.Sqrt(bMagnitude));
        }

        /// <summary>
        ///   Gets terms frequency for the specified text.
        /// </summary>
        /// <param name="text">The text to be parsed.</param>
        /// <returns>Dictionary containing words and count of occurrence.</returns>
        static private Dictionary<string, uint> GetTermsOccurrence(string text)
        {
            Dictionary<string, uint> termsFrequency = new Dictionary<string, uint>(text.Length);
            string filteredText = Regex.Replace(text, @"^File:|(img\d+|\d+)?\.\w+$|\s\d+\s", string.Empty);

            string[] words = filteredText.ToLower().Split(new char[] { ' ', ',', '.', '-', '_', '!', '?', ':' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (termsFrequency.ContainsKey(word))
                    ++termsFrequency[word];
                else
                    termsFrequency.Add(word, 1);
            }

            return termsFrequency;
        }
    }
}
