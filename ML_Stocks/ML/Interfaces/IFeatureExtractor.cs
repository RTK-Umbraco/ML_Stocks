namespace ML_Stocks.ML.Interfaces
{
    public interface IFeatureExtractor
    {
        void FilterAndSaveCSV(string filePath);
    }
}
