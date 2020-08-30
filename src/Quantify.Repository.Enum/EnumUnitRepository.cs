using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Validators;
using System;
using System.Globalization;
using System.Reflection;

namespace Quantify.Repository.Enum
{
    public class EnumUnitRepository<TUnit> : UnitRepository<TUnit> where TUnit : struct, IConvertible
    {
        private readonly Type unitEnumType = typeof(TUnit);
        private readonly TUnit baseUnit;

        public EnumUnitRepository()
        {
            if (new GenericEnumParametersValidator().GenericParameterIsEnumType<TUnit>() == false)
                throw new GenericArgumentException("The generic argument is not valid. Expected an enum.", nameof(TUnit), typeof(TUnit));

            var baseUnitAttribute = unitEnumType.GetCustomAttribute<BaseUnitAttribute>(false);

            if (baseUnitAttribute == null)
                throw new InvalidUnitEnumException("The unit enum is missing the base unit attribute. See the documentation for more information.", typeof(TUnit));

            if (System.Enum.GetName(unitEnumType, baseUnitAttribute.BaseUnit) == null)
                throw new InvalidUnitEnumException("The unit enum base unit attribute is not pointing to a valid value in the enum.", typeof(TUnit));

            baseUnit = (TUnit)baseUnitAttribute.BaseUnit;
        }

        public double? GetUnitConversionRate(TUnit unit)
        {
            if (unit.Equals(baseUnit))
                return 1;

            var unitAttribute = GetUnitAttributeByUnit(unit);

            if (unitAttribute == null)
                return null;

            return double.TryParse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : (double?)null;
        }

        public decimal? GetPreciseUnitConversionRate(TUnit unit)
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
            return unitEnumType.GetField(System.Enum.GetName(unitEnumType, unit)).GetCustomAttribute<UnitAttribute>(false);
        }
    }
}