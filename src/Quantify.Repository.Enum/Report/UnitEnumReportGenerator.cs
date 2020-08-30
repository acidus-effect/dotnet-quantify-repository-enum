using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Validators;
using System;
using System.Linq;
using System.Reflection;

namespace Quantify.Repository.Enum.Report
{
    public class UnitEnumReportGenerator
    {
        public UnitEnumReport CreateReport<TUnit>() where TUnit : struct, IConvertible
        {
            if (new GenericEnumParametersValidator().GenericParameterIsEnumType<TUnit>() == false)
                throw new GenericArgumentException("The generic argument is not valid. Expected an enum.", nameof(TUnit), typeof(TUnit));

            var hasValueMissingUnitAttribute = false;
            var hasValueWithInvalidUnitAttribute = false;
            var baseUnitHasUnitAttribute = false;
            var isMissingBaseUnitAttribute = false;
            var hasInvalidBaseUnitAttribute = false;

            var enumType = typeof(TUnit);

            var baseUnitAttribute = enumType.GetCustomAttribute<BaseUnitAttribute>(false);
            var baseUnitValueName = baseUnitAttribute == null ? null : System.Enum.GetName(enumType, baseUnitAttribute.BaseUnit);

            isMissingBaseUnitAttribute = baseUnitAttribute == null;

            if (baseUnitAttribute != null)
                hasInvalidBaseUnitAttribute = baseUnitValueName == null;

            foreach (var unitEnumValue in enumType.GetEnumValues().OfType<TUnit>())
            {
                var unitAttribute = enumType.GetField(System.Enum.GetName(enumType, unitEnumValue)).GetCustomAttribute<UnitAttribute>(false);
                var unitIsBaseUnit = baseUnitValueName == null ? false : unitEnumValue.Equals(System.Enum.Parse(enumType, baseUnitValueName));

                baseUnitHasUnitAttribute = baseUnitHasUnitAttribute || (unitIsBaseUnit && unitAttribute != null);
                hasValueMissingUnitAttribute = hasValueMissingUnitAttribute || (unitIsBaseUnit == false && unitAttribute == null);

                if (unitAttribute != null)
                {
                    var canParseToDecimal = decimal.TryParse(unitAttribute.ConversionValue, out var decimalValue);
                    var canParseToDouble = double.TryParse(unitAttribute.ConversionValue, out var doubleValue);

                    hasValueWithInvalidUnitAttribute = hasValueWithInvalidUnitAttribute || canParseToDecimal == false || canParseToDouble == false;
                }
            }

            return new UnitEnumReport(hasValueMissingUnitAttribute, hasValueWithInvalidUnitAttribute, baseUnitHasUnitAttribute, isMissingBaseUnitAttribute, hasInvalidBaseUnitAttribute);
        }
    }
}