using Microsoft.ML;
using ML_Stocks.ML.Base;
using ML_Stocks.ML.Objects;
using Newtonsoft.Json;

namespace ML_Stocks.ML
{
    public class Predictor : BaseML
    {
        public void Predict(string inputDataFile)
        {
            if (!File.Exists(inputDataFile))
            {
                Console.WriteLine($"Failed to find input data at {inputDataFile}");
                return;
            }

            var mlModel = LoadMLModel();

            var predictionEngine = MlContext.Model.CreatePredictionEngine<StockHistory, StockHistoryPrediction>(mlModel);

            //Maybe remove json since we dont want to predict ourself
            var json = File.ReadAllText(inputDataFile);

            var prediction = predictionEngine.Predict(JsonConvert.DeserializeObject<StockHistory>(json));

            Console.WriteLine($"Based on input json:{Environment.NewLine}" +
                              $"{json}{Environment.NewLine}" +
                              $"The closing price of is predicted to '{prediction.Close:#.##}' on date: '{DateTime.Today.AddDays(1)}'");
        }

        private ITransformer LoadMLModel()
        {
            ITransformer mlModel;

            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                mlModel = MlContext.Model.Load(stream, out _);
            }
            if (mlModel == null)
            {
                //Throw exceptions
                Console.WriteLine("Failed to load model");
            }

            return mlModel;
        }
    }
}
