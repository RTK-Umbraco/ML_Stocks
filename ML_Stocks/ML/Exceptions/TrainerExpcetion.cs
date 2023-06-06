namespace ML_Stocks.ML.Exceptions
{
    public class TrainerExpcetion : Exception
    {
        public TrainerExpcetion() : base() { }
        public TrainerExpcetion(string message) : base(message) { }
        public TrainerExpcetion(string message, Exception inner) : base(message, inner) { }
    }
}
