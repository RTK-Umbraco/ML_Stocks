using ML_Stocks.ML;

namespace ML_Stocks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Hvad er funktionaliteten
            //Klasse diagram
            //Hvilken model vi bruger(træner)
            switch (args[0])
            {
                case "train":
                    new Trainer().Train(args[1]);
                    break;
                case "predict":
                    new Predictor().Predict(args[1]);
                    break;
                default:
                    Console.WriteLine($"{args[0]} is an invalid option");
                    break;
            }
        }
    }
}