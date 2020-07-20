using System;
using System.Globalization;

namespace Quantify.Repository.Enum.ValueParser
{
    internal class StringToDecimalValueParser : StringValueParser<decimal>
    {
        public decimal Parse(string valueString)
        {
            if (valueString == null)
                throw new ArgumentNullException(nameof(valueString));

            if (string.IsNullOrWhiteSpace(valueString))
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(valueString));

            return decimal.Parse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}