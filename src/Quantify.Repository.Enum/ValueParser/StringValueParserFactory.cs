namespace Quantify.Repository.Enum.ValueParser
{
    internal sealed class StringValueParserFactory<TValue>
    {
        public StringValueParser<TValue> Build()
        {
            if (typeof(TValue) == typeof(double))
                return (StringValueParser<TValue>)new StringToDoubleValueParser();

            if (typeof(TValue) == typeof(decimal))
                return (StringValueParser<TValue>)new StringToDecimalValueParser();

            throw new GenericArgumentException($"The type of the generic parameter is invalid. Expected {typeof(double).Name} or {typeof(decimal).Name}.", nameof(TValue), typeof(TValue));
        }
    }
}