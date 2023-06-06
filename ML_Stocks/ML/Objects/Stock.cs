using Microsoft.ML.Data;

namespace ML_Stocks.ML.Objects
{
    public class Stock
    {
        //[LoadColumn(0)]
        //public float Open { get; set; }

        //[LoadColumn(1)]
        //public float High { get; set; }

        //[LoadColumn(2)]
        //public float Low { get; set; }

        //[LoadColumn(3)]
        //public float Close { get; set; }

        //[LoadColumn(4)]
        //public int Volume { get; set; }

        [LoadColumn(0)]
        public DateTime Date { get; set; }

        [LoadColumn(1)]
        public float Close { get; set; }
    }
}
