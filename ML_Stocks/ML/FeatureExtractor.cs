using ML_Stocks.ML.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML_Stocks.ML
{
    public class FeatureExtractor
    {
        public void FilterAndSaveCSV(string filePath)
        {
            // Read all lines from the CSV file
            var lines = File.ReadAllLines(filePath);

            // Determine the separator used in the CSV file
            var separator = DetermineSeparator(lines[0]);

            // Extract headers and remove unwanted column names
            var headers = lines[0].Split(separator);
            var filteredHeaders = headers.Where(header =>
                header != StockColumns.Open &&
                header != StockColumns.Low &&
                header != StockColumns.High &&
                header != StockColumns.AdjClose &&
                header != StockColumns.Volume);

            // Create a list to store the filtered rows
            var filteredRows = new List<string>();

            // Add the filtered headers to the list of rows
            filteredRows.Add(string.Join(separator, filteredHeaders));

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
             index != Array.IndexOf(headers, StockColumns.Volume))
             .Select((column, index) =>
                 index == Array.IndexOf(headers, StockColumns.Close) ? FormatCloseValue(column) : column);


                // Add the filtered row to the list
                filteredRows.Add(string.Join(separator, filteredRow));
            }

            // Save the filtered data back to the CSV file
            File.WriteAllLines(filePath, filteredRows);
        }

        private string FormatCloseValue(string value)
        {
            if (decimal.TryParse(value, out decimal closeValue))
            {
                return closeValue.ToString("0.###");
            }
            else
            {
                return value;
            }
        }

        private char DetermineSeparator(string headerRow)
        {
            return headerRow.Contains(',') ? ',' : ';';
        }
    }
}
