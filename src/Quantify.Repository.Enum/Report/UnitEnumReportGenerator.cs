using Quantify.Repository.Enum.Validators;
using System.Linq;
using System.Reflection;

namespace Quantify.Repository.Enum.Report
{
    /// <summary>
    /// Generator class used to create instances of <see cref="UnitEnumReport"/> with results of the analysis of a unit enum.
    /// </summary>
    /// <remarks>
    /// Only critical errors in a unit enum will make the instantiation of a <see cref="UnitRepository{TUnit}" /> fail. Minor things like missing or invalid <see cref="UnitAttribute"/> attributes, will not result in an error, but the enum value will be ignored instead.
    /// This report generator returns a report with warnings and errors found when analysing a given unit enum. This result can be used in unit tests, to make sure that a unit enum is configured in the correct way.
    /// </remarks>
    public class UnitEnumReportGenerator
    {
        /// <summary>
        /// Create a analysis report for a given unit enum.
        /// </summary>
        /// <typeparam name="TUnit">The unit enum to analyse.</typeparam>
        /// <returns>A unit enum report with the result of the analysis of the enm referenced in <typeparamref name="TUnit"/>.</returns>
        /// <exception cref="GenericArgumentException"><typeparamref name="TUnit"/> is not an <code>enum</code>.</exception>
        public UnitEnumReport CreateReport<TUnit>() where TUnit : struct
        {
            if (new GenericEnumParametersValidator().GenericParameterIsEnumType<TUnit>() == false)
                throw new GenericArgumentException("The generic argument is not valid. Expected an enum.", nameof(TUnit), typeof(TUnit));

            var hasValueMissingUnitAttribute = false;
            var hasValueWithInvalidUnitAttribute = false;
            var baseUnitHasUnitAttribute = false;
            var isMissingEnumUnitAttribute = false;
            var hasInvalidEnumUnitAttribute = false;

            var enumType = typeof(TUnit);

            var enumUnitAttribute = enumType.GetTypeInfo().GetCustomAttribute<UnitEnumAttribute>(false);
            var baseUnitValueName = enumUnitAttribute == null ? null : System.Enum.GetName(enumType, enumUnitAttribute.BaseUnit);

            isMissingEnumUnitAttribute = enumUnitAttribute == null;

            if (enumUnitAttribute != null)
                hasInvalidEnumUnitAttribute = baseUnitValueName == null;

            foreach (var unitEnumValue in System.Enum.GetValues(enumType).OfType<TUnit>())
            {
                var unitAttribute = enumType.GetRuntimeField(System.Enum.GetName(enumType, unitEnumValue)).GetCustomAttribute<UnitAttribute>(false);
                var unitIsBaseUnit = baseUnitValueName == null ? false : unitEnumValue.Equals(System.Enum.Parse(enumType, baseUnitValueName));

                baseUnitHasUnitAttribute = baseUnitHasUnitAttribute || (unitIsBaseUnit && unitAttribute != null);
                hasValueMissingUnitAttribute = hasValueMissingUnitAttribute || (unitIsBaseUnit == false && unitAttribute == null);

                if (unitAttribute != null)
                {
                    var canParseToDecimal = decimal.TryParse(unitAttribute.ValueInBaseUnits, out var decimalValue);
                    var canParseToDouble = double.TryParse(unitAttribute.ValueInBaseUnits, out var doubleValue);

                    hasValueWithInvalidUnitAttribute = hasValueWithInvalidUnitAttribute || canParseToDecimal == false || canParseToDouble == false;
                }
            }

            return new UnitEnumReport(hasValueMissingUnitAttribute, hasValueWithInvalidUnitAttribute, baseUnitHasUnitAttribute, isMissingEnumUnitAttribute, hasInvalidEnumUnitAttribute);
        }
    }
}