using Microsoft.ML.Data;

namespace ML_Stocks.ML.Objects
{
    public class StockPrediction
    {
        //[ColumnName("ForecastedClose")]
        public float[] ForecastedClose { get; set; }

        public float[] LowerBound { get; set; }

        public float[] UpperBound { get; set; }
    }
}
