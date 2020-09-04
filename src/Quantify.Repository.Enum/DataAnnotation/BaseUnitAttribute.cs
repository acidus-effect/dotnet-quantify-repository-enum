using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    /// <summary>
    /// Specifies that an enum contains quantity unit values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public sealed class BaseUnitAttribute : Attribute
    {
        /// <summary>
        /// Get the reference to the enum value that represents the base unit.
        /// </summary>
        public object BaseUnit { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUnitAttribute"/> class with a reference to the enum value that represents the base unit.
        /// </summary>
        /// <param name="baseUnit">The reference to the enum value that represents the base unit</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUnit"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException"><paramref name="baseUnit"/> is not an <code>enum</code>.</exception>
        public BaseUnitAttribute(object baseUnit)
        {
            if (baseUnit == null)
                throw new ArgumentNullException(nameof(baseUnit));

            if (baseUnit.GetType().IsEnum == false)
                throw new ArgumentException("The argument must be of the type enum.", nameof(baseUnit));

            BaseUnit = baseUnit;
        }
    }
}