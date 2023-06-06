namespace ML_Stocks.ML.Exceptions
{
    public class PredictorExpcetion : Exception
    {
        public PredictorExpcetion() : base() { }
        public PredictorExpcetion(string message) : base(message) { }
        public PredictorExpcetion(string message, Exception inner) : base(message, inner) { }
    }
}
