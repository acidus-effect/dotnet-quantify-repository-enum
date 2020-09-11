using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Validators;
using System;
using System.Globalization;
using System.Reflection;

namespace Quantify.Repository.Enum
{
    /// <summary>
    /// Enum based implementation of the unit repository interface.
    /// </summary>
    /// <remarks>
    /// An enum is used to define the available units.
    /// 
    /// Unit values annotated with the <see cref="UnitAttribute"/> attribute are considered valid units, if the supplied conversion value can be parsed to both a <see cref="decimal"/> and a <see cref="double"/>.
    /// A unit value will be ignored, if the value is missing the <see cref="UnitAttribute"/> attribute or if the conversion value cannot be parsed without errors.
    /// 
    /// Every unit enum must have a base unit. A base unit is defined by annotating the enum with the <see cref="BaseUnitAttribute"/> attribute. If this attribute is missing or if it doesn't point to a value within the enum, the instansiation of the repository will fail.
    /// 
    /// If the base unit value is annotated with the <see cref="UnitAttribute"/> attribute, the speficied conversion value will be ignored, since the base unit always have a conversion value of 1.
    /// </remarks>
    /// <typeparam name="TUnit">The enum type containing unit data.</typeparam>
    public class EnumUnitRepository<TUnit> : UnitRepository<TUnit> where TUnit : struct
    {
        private readonly Type unitEnumType = typeof(TUnit);
        private readonly TUnit baseUnit;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumUnitRepository"/> class.
        /// </summary>
        /// <exception cref="GenericArgumentException">The generic argument <typeparamref name="TUnit"/> is not an enum.</exception>
        /// <exception cref="InvalidUnitEnumException">The unit enum <typeparamref name="TUnit"/> is not annotated with the <see cref="BaseUnitAttribute"/> attribute -or- The <see cref="BaseUnitAttribute"/> on the unit enum <typeparamref name="TUnit"/> is pointing to an invalid base unit.</exception>
        public EnumUnitRepository()
        {
            if (new GenericEnumParametersValidator().GenericParameterIsEnumType<TUnit>() == false)
                throw new GenericArgumentException("The generic argument is not valid. Expected an enum.", nameof(TUnit), typeof(TUnit));

            var baseUnitAttribute = unitEnumType.GetTypeInfo().GetCustomAttribute<BaseUnitAttribute>(false);

            if (baseUnitAttribute == null)
                throw new InvalidUnitEnumException("The unit enum is missing the base unit attribute. See the documentation for more information.", typeof(TUnit));

            if (System.Enum.GetName(unitEnumType, baseUnitAttribute.BaseUnit) == null)
                throw new InvalidUnitEnumException("The unit enum base unit attribute is not pointing to a valid value in the enum.", typeof(TUnit));

            baseUnit = (TUnit)baseUnitAttribute.BaseUnit;
        }

        /// <inheritdoc/>
        public double? GetUnitValueInBaseUnits(TUnit unit)
        {
            if (unit.Equals(baseUnit))
                return 1;

            var unitAttribute = GetUnitAttributeByUnit(unit);

            if (unitAttribute == null)
                return null;

            return double.TryParse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : (double?)null;
        }

        /// <inheritdoc/>
        public decimal? GetPreciseUnitValueInBaseUnits(TUnit unit)
        {
            if (unit.Equals(baseUnit))
                return 1;

            var unitAttribute = GetUnitAttributeByUnit(unit);

            if (unitAttribute == null)
                return null;

            return decimal.TryParse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : (decimal?)null;
        }

        private UnitAttribute GetUnitAttributeByUnit(TUnit unit)
        {
            return unitEnumType.GetRuntimeField(System.Enum.GetName(unitEnumType, unit)).GetCustomAttribute<UnitAttribute>(false);
        }
    }
}