using System;
using System.Globalization;

namespace Quantify.Repository.Enum.ValueParser
{
    internal class StringToDoubleValueParser : StringValueParser<double>
    {
        public double Parse(string valueString)
        {
            if (valueString == null)
                throw new ArgumentNullException(nameof(valueString));

            if (string.IsNullOrWhiteSpace(valueString))
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(valueString));

            return double.Parse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}