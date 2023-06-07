using ML_Stocks.Helpers;
using ML_Stocks.ML.Objects;

namespace ML_Stocks.ML
{
    public class FeatureExtractor
    {
        public void FilterAndSaveCSV(string filePath)
        {
            // Read all lines from the CSV file
            var lines = File.ReadAllLines(filePath);

            // Determine the separator used in the CSV file
            var separator = SeparatorHelper.DetermineSeparator(lines[0]);

            // Extract headers and remove unwanted column names
            var headers = lines[0].Split(separator);
            var filteredHeaders = headers.Where(header =>
                header != StockColumns.Open &&
                header != StockColumns.Low &&
                header != StockColumns.High &&
                header != StockColumns.AdjClose &&
                header != StockColumns.Volume);

            // Create a list to store the filtered rows
            var filteredRows = new List<string>
            {
                // Add the filtered headers to the list of rows
                string.Join(separator, filteredHeaders)
            };

            // Iterate through the remaining lines and filter the columns
            for (int i = 1; i < lines.Length; i++)
            {
                var row = lines[i].Split(separator);

                // Remove unwanted columns
                var filteredRow = row.Where((column, index) =>
             index != Array.IndexOf(headers, StockColumns.Open) &&
             index != Array.IndexOf(headers, StockColumns.Low) &&
             index != Array.IndexOf(headers, StockColumns.High) &&
             index != Array.IndexOf(headers, StockColumns.AdjClose) &&
             index != Array.IndexOf(headers, StockColumns.Volume));

                // Add the filtered row to the list
                filteredRows.Add(string.Join(separator, filteredRow));
            }

            // Save the filtered data back to the CSV file
            File.WriteAllLines(filePath, filteredRows);
        }
    }
}
