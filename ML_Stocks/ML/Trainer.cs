using Microsoft.ML;
using ML_Stocks.Common;
using ML_Stocks.ML.Base;
using ML_Stocks.ML.Objects;

namespace ML_Stocks.ML
{
    public class Trainer : BaseML
    {
        public void Train(string trainingFileName)
        {
            if (!File.Exists(trainingFileName))
            {
                Console.WriteLine($"Failed to find training data file ({trainingFileName}");
                return;
            }

            var trainingDataView = MlContext.Data.LoadFromTextFile<StockHistory>(trainingFileName, ',', hasHeader: true);

            var dataSplit = MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

            var dataProcessPipeline = MlContext.Transforms.CopyColumns("Label", nameof(StockHistory.Date))
                .Append(MlContext.Transforms.NormalizeMeanVariance(nameof(StockHistory.Close)))
                .Append(MlContext.Transforms.Concatenate("Features", typeof(StockHistory).ToPropertyList<StockHistory>(nameof(StockHistory.Date))));

            var trainer = MlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features");

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);

            MlContext.Model.Save(trainedModel, dataSplit.TrainSet.Schema, ModelPath);

            var testSetTransform = trainedModel.Transform(dataSplit.TestSet);

            var modelMetrics = MlContext.Regression.Evaluate(testSetTransform);
            Console.WriteLine($"Loss Function: {modelMetrics.LossFunction:0.##}{Environment.NewLine}" +
             $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError:#.##}{Environment.NewLine}" +
             $"Mean Squared Error: {modelMetrics.MeanSquaredError:#.##}{Environment.NewLine}" +
             $"RSquared: {modelMetrics.RSquared:0.##}{Environment.NewLine}" +
             $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError:#.##}");
        }
    }
}
