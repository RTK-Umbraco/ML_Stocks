using Microsoft.ML;
using ML_Stocks.ML.Base;
using ML_Stocks.ML.Objects;
using Microsoft.ML.Transforms.TimeSeries;
using ML_Stocks.Common;
using ML_Stocks.ML.Exceptions;
using ML_Stocks.Helpers;
using ML_Stocks.ML.Interfaces;

namespace ML_Stocks.ML
{
    public class Predictor : BaseML, IPredictor
    {
        public void Predict(string inputDataFile)
        {
            try
            {
                FileHelper.ValidateFileExists(Constants.MODEL_FILENAME);

                FileHelper.ValidateFileExists(inputDataFile);

                var mlModel = LoadMLModel();

                var predictionEngine = mlModel.CreateTimeSeriesEngine<Stock, StockPrediction>(MlContext);

                var json = File.ReadAllText(inputDataFile);

                var stock = JsonHelper.DeserializeJson<Stock>(json);

                var prediction = predictionEngine.Predict(stock);

                Console.WriteLine($"Given a stock price of ${stock.Close}, the next closing price are predicted to be: '{string.Join(", ", prediction.ForecastedClose)}' on the date: {stock.Date.ToString("MM/dd/yyyy")}");
                Console.WriteLine($"Lower confidence: {string.Join(", ", prediction.LowerBound)}");
                Console.WriteLine($"Upper confidence: {string.Join(", ", prediction.UpperBound)}");
            }
            catch (NullReferenceException nullReferenceException)
            {
                throw new PredictorExpcetion("A null reference exception occurred.", nullReferenceException);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                throw new PredictorExpcetion("An error occurred while fetching the file.", fileNotFoundException);
            }
            catch (Exception exception)
            {
                throw new PredictorExpcetion("An error occurred while predicting", exception);
            }
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
                throw new NullReferenceException("Failed to load model");
            }

            return mlModel;
        }
    }
}
