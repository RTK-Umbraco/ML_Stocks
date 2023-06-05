using Microsoft.ML;
using ML_Stocks.Common;
using ML_Stocks.ML.Base;
using ML_Stocks.ML.Objects;
using Microsoft.ML.Transforms.TimeSeries;
using Microsoft.ML.Data;
using System.Data;
using System.Reflection;


namespace ML_Stocks.ML
{
    public class Trainer : BaseML
    {
        public void Train(string trainingFileName)
        {
            DoesModelFileNameExist();

            DoesTrainingFileNameExist(trainingFileName);

            var trainingDataView = MlContext.Data.LoadFromTextFile<Stock>(trainingFileName, ',', hasHeader: true);

            var dataProcessPipeline = MlContext.Forecasting.ForecastBySsa(
                outputColumnName: nameof(StockPrediction.Forecast),
                inputColumnName: nameof(Stock.Close),
                windowSize: 7,
                seriesLength: 30,
                trainSize: 24,
                horizon: 1,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: nameof(StockPrediction.LowerBound),
                confidenceUpperBoundColumn: nameof(StockPrediction.UpperBound));

            //var customTransformer = MlContext.Transforms.CustomMapping(parseDateTime, null).Fit(trainingDataView);
            var transformer = dataProcessPipeline.Fit(trainingDataView);

            var forecastEngine = transformer.CreateTimeSeriesEngine<Stock, StockPrediction>(MlContext);

            forecastEngine.CheckPoint(MlContext, Constants.MODEL_FILENAME);

            Console.WriteLine($"Wrote model to {Constants.MODEL_FILENAME}");
        }
    }
}
