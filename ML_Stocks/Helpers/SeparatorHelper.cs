namespace ML_Stocks.Helpers
{
    public static class SeparatorHelper
    {
        public static char DetermineSeparator(string[] lines)
        {
            char[] possibleSeparators = { ',', ';', '\t' };

            Dictionary<char, int> separatorCounts = new Dictionary<char, int>();

            foreach (char separator in possibleSeparators)
            {
                int separatorCount = 0;

                foreach (string line in lines)
                {
                    separatorCount += line.Split(separator).Length - 1;
                }

                separatorCounts.Add(separator, separatorCount);
            }

            char mostFrequentSeparator = separatorCounts.OrderByDescending(x => x.Value).FirstOrDefault().Key;

            Console.WriteLine($"The separator used in the CSV file is: '{mostFrequentSeparator}'");   
            return mostFrequentSeparator;
        }
    }
}
