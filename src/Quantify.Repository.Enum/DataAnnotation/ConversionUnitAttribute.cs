using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class QuantityUnitAttribute : Attribute
    {
        public string ConversionValue { get; }

        public QuantityUnitAttribute(string conversionRate)
        {
            ConversionValue = conversionRate;
        }
    }
}