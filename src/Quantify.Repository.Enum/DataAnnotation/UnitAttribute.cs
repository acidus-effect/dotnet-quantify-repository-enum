using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnitAttribute : Attribute
    {
        public string ConversionValue { get; }

        public UnitAttribute(string conversionRate)
        {
            if (conversionRate == null)
                throw new ArgumentNullException(nameof(conversionRate));

            if (string.IsNullOrWhiteSpace(conversionRate))
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(conversionRate));

            ConversionValue = conversionRate;
        }
    }
}