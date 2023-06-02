namespace ML_Stocks.Common
{
    public static class ExtensionMethods
    {
        public static string[] ToPropertyList<T>(this Type type, string label)
        {
            var properties = type.GetProperties();

            var propertiesName = properties.Where(x => x.Name != label).Select(x => x.Name).ToArray();

            return propertiesName;
        }
    }
}
