using ML_Stocks.ML;
using ML_Stocks.ML.Interfaces;

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
                    ITrainer trainer = new Trainer();
                    trainer.Train(args[1]);

                    new Trainer().Train(args[1]);
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