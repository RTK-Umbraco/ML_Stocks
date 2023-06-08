using Microsoft.ML;
using ML_Stocks.Common;
using ML_Stocks.ML.Base;
using ML_Stocks.ML.Objects;
using Microsoft.ML.Transforms.TimeSeries;
using ML_Stocks.ML.Exceptions;
using ML_Stocks.Helpers;
using ML_Stocks.ML.Interfaces;

namespace ML_Stocks.ML
{
    public class Trainer : BaseML, ITrainer
    {
        public void Train(string trainingFileName)
        {
            try
            {
                FileHelper.ValidateFileExists(Constants.MODEL_FILENAME);

                FileHelper.ValidateFileExists(trainingFileName);

                var lines = File.ReadAllLines(trainingFileName);

                var separator = SeparatorHelper.DetermineSeparator(lines);

                var trainingDataView = MlContext.Data.LoadFromTextFile<Stock>(trainingFileName, separator, hasHeader: true);

                var dataProcessPipeline = MlContext.Forecasting.ForecastBySsa(
                    outputColumnName: nameof(StockPrediction.ForecastedClose),
                    //Inputcolumn determines the data being machined learned
                    inputColumnName: nameof(Stock.Close),
                    windowSize: 10,
                    seriesLength: 250,
                    trainSize: 250,
                    horizon: 1,
                    confidenceLevel: 0.95f,
                    confidenceLowerBoundColumn: nameof(StockPrediction.LowerBound),
                    confidenceUpperBoundColumn: nameof(StockPrediction.UpperBound));

                var transformer = dataProcessPipeline.Fit(trainingDataView);

                var forecastEngine = transformer.CreateTimeSeriesEngine<Stock, StockPrediction>(MlContext);

                forecastEngine.CheckPoint(MlContext, Constants.MODEL_FILENAME);

                Console.WriteLine($"Wrote model to {Constants.MODEL_FILENAME}");
            }
            catch (Exception exception)
            {
                throw new TrainerExpcetion("An error occurred while training", exception);
            }
        }
    }
}
