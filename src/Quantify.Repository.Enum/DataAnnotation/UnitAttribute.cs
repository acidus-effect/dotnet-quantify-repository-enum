using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class UnitAttribute : Attribute
    {
        public string ConversionValue { get; }

        public UnitAttribute(string conversionRate)
        {
            ConversionValue = conversionRate;
        }
    }
}