using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    /// <summary>
    /// Specifies that an enum value can be used as a quantity unit.
    /// </summary>
    /// <remarks>
    /// <see cref="ValueInBaseUnits"/> describes 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnitAttribute : Attribute
    {
        /// <summary>
        /// Get the number of base units this unit represents.
        /// </summary>
        public string ValueInBaseUnits { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAttribute"/> class with a unit conversion value.
        /// </summary>
        /// <param name="valueInBaseUnits">The number of base units this unit represents</param>
        /// <exception cref="ArgumentNullException"><paramref name="valueInBaseUnits"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException"><paramref name="valueInBaseUnits"/> is empty or contains only whitespaces.</exception>
        public UnitAttribute(string valueInBaseUnits)
        {
            if (valueInBaseUnits == null)
                throw new ArgumentNullException(nameof(valueInBaseUnits));

            if (string.IsNullOrWhiteSpace(valueInBaseUnits))
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(valueInBaseUnits));

            ValueInBaseUnits = valueInBaseUnits;
        }
    }
}