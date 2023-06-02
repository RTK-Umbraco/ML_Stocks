using Microsoft.ML.Data;

namespace ML_Stocks.ML.Objects
{
    public class StockHistory
    {
        [LoadColumn(0)]
        public float Date { get; set; }

        [LoadColumn(1)]
        public float Close { get; set; }
    }
}
