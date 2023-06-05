namespace ML_Stocks.ML.Objects
{
    public class StockPrediction
    {
        public float[] Forecast { get; set; }

        public float[] LowerBound { get; set; }

        public float[] UpperBound { get; set; }

        public float Date { get; set; }
        public float Close { get; set; }

        //Add prop that tell you if its is a good choice to buy stocks
    }
}
