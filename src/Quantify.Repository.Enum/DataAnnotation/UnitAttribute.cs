using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    /// <summary>
    /// Specifies that an enum value can be used as a quantity unit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnitAttribute : Attribute
    {
        /// <summary>
        /// Get the conversion value of the unit.
        /// </summary>
        public string ConversionValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAttribute"/> class with a unit conversion value.
        /// </summary>
        /// <param name="conversionValue">The conversion value of the unit</param>
        /// <exception cref="ArgumentNullException"><paramref name="conversionValue"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException"><paramref name="conversionValue"/> is empty or contains only whitespaces.</exception>
        public UnitAttribute(string conversionValue)
        {
            if (conversionValue == null)
                throw new ArgumentNullException(nameof(conversionValue));

            if (string.IsNullOrWhiteSpace(conversionValue))
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(conversionValue));

            ConversionValue = conversionValue;
        }
    }
}