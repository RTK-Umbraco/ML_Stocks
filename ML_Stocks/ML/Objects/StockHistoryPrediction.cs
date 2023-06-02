using Microsoft.ML.Data;

namespace ML_Stocks.ML.Objects
{
    public class StockHistoryPrediction
    {
        [ColumnName("StockPrediction")]
        public float Date { get; set; }
        public float Close { get; set; }

        //Add prop that tell you if its is a good choice to buy stocks
    }
}
