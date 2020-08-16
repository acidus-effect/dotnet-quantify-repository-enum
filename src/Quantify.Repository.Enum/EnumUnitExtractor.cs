using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Validators;
using Quantify.Repository.Enum.ValueParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantify.Repository.Enum
{
    internal class EnumUnitExtractor<TValue, TUnit> where TUnit : struct, IConvertible
    {
        private readonly StringValueParser<TValue> stringValueParser;

        public EnumUnitExtractor(StringValueParser<TValue> stringValueParser)
        {
            if (new GenericEnumParametersValidator().GenericParameterIsEnumType<TUnit>() == false)
                throw new GenericArgumentException("The generic argument is not valid. Expected an enum.", nameof(TUnit), typeof(TUnit));

            this.stringValueParser = stringValueParser ?? throw new ArgumentNullException(nameof(stringValueParser));
        }

        internal virtual IReadOnlyDictionary<TUnit, TValue> Extract()
        {
            var report = CreateExtractionReport();

            if (report.HasErrors)
            {
                var errorStringBuilder = new StringBuilder();

                errorStringBuilder.AppendLine($"One or more issues were encountered while attempting to process the provided unit enum:");
                errorStringBuilder.AppendLine();
                errorStringBuilder.AppendLine(report.CreateFormatedWarningsAndErrorsString());
                errorStringBuilder.AppendLine();
                errorStringBuilder.Append("See the documentation for more information.");

                throw new InvalidUnitEnumException(errorStringBuilder.ToString(), typeof(TUnit));
            }

            return report.ValidUnitEnumValuesDictionary;
        }

        public EnumUnitExtractionReport<TUnit, TValue> CreateExtractionReport()
        {
            var validUnitEnumValuesDictionary = new Dictionary<TUnit, TValue>();
            var enumValuesMarkedAsBaseUnits = new HashSet<TUnit>();
            var enumValuesMissingAttributes = new HashSet<TUnit>();
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<TUnit>();

            var enumType = typeof(TUnit);

            foreach (var enumValue in System.Enum.GetValues(enumType).OfType<TUnit>())
            {
                var enumValueAttributes = enumType.GetField(System.Enum.GetName(enumType, enumValue)).GetCustomAttributes(false);

                var unitAttribute = enumValueAttributes.FirstOrDefault(attribute => attribute is UnitAttribute) as UnitAttribute;
                var baseUnitAttribute = enumValueAttributes.FirstOrDefault(attribute => attribute is BaseUnitAttribute) as BaseUnitAttribute;

                if (unitAttribute == null && baseUnitAttribute == null)
                    enumValuesMissingAttributes.Add(enumValue);

                if (unitAttribute != null && baseUnitAttribute != null)
                    enumValuesWithBothBaseUnitAndValueAttribute.Add(enumValue);

                if (unitAttribute != null)
                    validUnitEnumValuesDictionary[enumValue] = stringValueParser.Parse(unitAttribute.ConversionValue);

                if (baseUnitAttribute != null)
                {
                    validUnitEnumValuesDictionary[enumValue] = stringValueParser.Parse(1.ToString());
                    enumValuesMarkedAsBaseUnits.Add(enumValue);
                }
            }

            return new EnumUnitExtractionReport<TUnit, TValue>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);
        }
    }
}