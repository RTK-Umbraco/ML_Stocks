using ML_Stocks.Common;
using Microsoft.ML;
using ML_Stocks.ML.Exceptions;

namespace ML_Stocks.ML.Base
{
    public abstract class BaseML
    {
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, Constants.MODEL_FILENAME);
        protected readonly MLContext MlContext;
        protected BaseML()
        {
            MlContext = new MLContext(2020);
        }
    }
}
