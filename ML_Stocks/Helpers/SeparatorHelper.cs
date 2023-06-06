namespace ML_Stocks.Helpers
{
    public static class SeparatorHelper
    {
        public static char DetermineSeparator(string headerRow)
        {
            return headerRow.Contains(',') ? ',' : ';';
        }
    }
}
