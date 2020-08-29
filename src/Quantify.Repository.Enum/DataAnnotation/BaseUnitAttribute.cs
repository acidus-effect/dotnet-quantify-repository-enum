using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public sealed class BaseUnitAttribute : Attribute
    {
        public object BaseUnit { get; }

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