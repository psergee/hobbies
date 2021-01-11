namespace TitlesSimilarity.ML.Metrics
{
    public interface ISimilarity<T>
    {
        double Similarity(T a, T b);
    }
}