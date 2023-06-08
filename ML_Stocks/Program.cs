using ML_Stocks.ML;
using ML_Stocks.ML.Interfaces;

namespace ML_Stocks
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Netflix Given a stock price of $399.76, the next closing price are predicted to be: '395.79114' on the date: 08/06/2023
            //Tesla Given a stock price of $224.57, the next closing price are predicted to be: '231.60175' on the date: 08/06/2023

            switch (args[0])
            {
                case "train":
                    ITrainer trainer = new Trainer();
                    trainer.Train(args[1]);
                    break;
                case "predict":
                    IPredictor predictor = new Predictor();
                    predictor.Predict(args[1]);
                    break;
                case "extract":
                    IFeatureExtractor featureExtractor = new FeatureExtractor();
                    featureExtractor.FilterAndSaveCSV(args[1]);
                    break;
                default:
                    Console.WriteLine($"{args[0]} is an invalid option");
                    break;
            }
        }
    }
}