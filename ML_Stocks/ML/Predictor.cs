using Microsoft.ML;
using ML_Stocks.ML.Base;
using ML_Stocks.ML.Objects;
using Newtonsoft.Json;
using Microsoft.ML.Transforms.TimeSeries;
using ML_Stocks.Common;

namespace ML_Stocks.ML
{
    public class Predictor : BaseML
    {
        public void Predict(string inputDataFile)
        {
            DoesModelFileNameExist();

            DoesInputDataFileExist(inputDataFile);

            var mlModel = LoadMLModel();

            var predictionEngine = mlModel.CreateTimeSeriesEngine<Stock, StockPrediction>(MlContext);

            var json = File.ReadAllText(inputDataFile);

            var stockPrice = JsonConvert.DeserializeObject<Stock>(json);

            var prediction = predictionEngine.Predict(stockPrice);

            //Console.WriteLine($"Date: {stockPrice.Date}");
            //Console.WriteLine($"Open: {stockPrice.Open}");
            Console.WriteLine($"Given a stock price of ${stockPrice.Close}, the next closing price are predicted to be: '{string.Join(", ", prediction.Forecast)}' on the date: {DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")}");
            Console.WriteLine($"Lower confidence: {string.Join(", ", prediction.LowerBound)}");
            Console.WriteLine($"Upper confidence: {string.Join(", ", prediction.UpperBound)}");
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
