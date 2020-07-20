namespace Quantify.Repository.Enum.ValueParser
{
    internal interface StringValueParser<out TValue>
    {
        TValue Parse(string valueString);
    }
}