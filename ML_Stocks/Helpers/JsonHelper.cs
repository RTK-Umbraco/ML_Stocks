using Newtonsoft.Json;

namespace ML_Stocks.Helpers
{
    public static class JsonHelper
    {
        public static T DeserializeJson<T>(string json)
        {
            try
            {
                T result = JsonConvert.DeserializeObject<T>(json);

                if (result == null)
                {
                    throw new NullReferenceException("Deserialization returned null.");
                }

                return result;
            }
            //ASK MIKKEL IF THIS IS GOOD PRACTICE
            catch (Exception ex)
            {
                throw new NullReferenceException("Deserialization returned null.", ex);
            }
        }
    }
}
