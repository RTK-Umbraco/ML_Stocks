using ML_Stocks.Common;
using Microsoft.ML;

namespace ML_Stocks.ML.Base
{
    public class BaseML
    {
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, Constants.MODEL_FILENAME);
        protected readonly MLContext MlContext;
        protected BaseML()
        {
            MlContext = new MLContext(2020);
        }


        //Consider to only create one protected method that return the error message if the file doesnt exists
        protected void DoesModelFileNameExist()
        {
            if (!File.Exists(Constants.MODEL_FILENAME))
            {
                Console.WriteLine($"Failed to find training data file ({Constants.MODEL_FILENAME})");

                return;
            }
        }

        protected void DoesTrainingFileNameExist(string trainingFileName)
        {
            if (!File.Exists(trainingFileName))
            {
                Console.WriteLine($"Failed to find training data file ({trainingFileName}");
                return;
            }
        }

        protected void DoesInputDataFileExist(string inputDataFile)
        {
            if (!File.Exists(inputDataFile))
            {
                Console.WriteLine($"Failed to find input data at {inputDataFile}");
                return;
            }
        }
    }
}
