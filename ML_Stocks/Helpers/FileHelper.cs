namespace ML_Stocks.Helpers
{
    public static class FileHelper
    {
        public static void ValidateFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Failed to find to find data file", filePath);
            }
        }
    }
}
