using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Validators;
using Quantify.Repository.Enum.ValueParser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public virtual IReadOnlyDictionary<TUnit, TValue> Extract()
        {
            var enumType = typeof(TUnit);
            var unitDictionary = new Dictionary<TUnit, TValue>();
            var extractionStatus = new EnumUnitExtractionStatus();
           
            foreach (var unit in System.Enum.GetValues(typeof(TUnit)).OfType<TUnit>())
            {
                var attributes = enumType.GetField(System.Enum.GetName(enumType, unit)).GetCustomAttributes(false);

                var unitAttribute = attributes.FirstOrDefault(attribute => attribute is UnitAttribute) as UnitAttribute;
                var baseUnitAttribute = attributes.FirstOrDefault(attribute => attribute is BaseUnitAttribute) as BaseUnitAttribute;

                if (unitAttribute == null && baseUnitAttribute == null)
                    extractionStatus.ValueIsMissingBothAttributes = true;

                if (unitAttribute != null && baseUnitAttribute != null)
                    extractionStatus.HasBothAttributes = true;

                if (baseUnitAttribute != null && extractionStatus.HasBaseUnit)
                    extractionStatus.HasMultipleBaseUnits = true;

                if (baseUnitAttribute != null)
                    extractionStatus.HasBaseUnit = true;

                if (unitAttribute != null)
                    unitDictionary[unit] = stringValueParser.Parse(unitAttribute.ConversionValue);

                if (baseUnitAttribute != null)
                    unitDictionary[unit] = stringValueParser.Parse(1.ToString());
            }

            if (extractionStatus.HasBaseUnit == false)
                extractionStatus.IsMissingBaseUnit = true;

            ThrowExceptionIfInvalid(extractionStatus);
            return new ReadOnlyDictionary<TUnit, TValue>(unitDictionary);
        }

        private void ThrowExceptionIfInvalid(EnumUnitExtractionStatus extractionStatus)
        {
            if (extractionStatus.HasBaseUnit && !extractionStatus.ValueIsMissingBothAttributes && !extractionStatus.HasBothAttributes && !extractionStatus.IsMissingBaseUnit && !extractionStatus.HasMultipleBaseUnits)
                return;

            var errorTypeCount = 0;
            var exceptionMessageBuilder = new StringBuilder();

            if (extractionStatus.ValueIsMissingBothAttributes)
            {
                errorTypeCount++;
                exceptionMessageBuilder.AppendLine(" - One or more of the enum values are missing the unit or base unit declaration attribute.");
            }

            if (extractionStatus.HasBothAttributes)
            {
                errorTypeCount++;
                exceptionMessageBuilder.AppendLine(" - One or more of the units defined in the enum, are defined as both a unit and a base unit.");
            }

            if (extractionStatus.IsMissingBaseUnit)
            {
                errorTypeCount++;
                exceptionMessageBuilder.AppendLine(" - Non of the units defined in the enum, are defined as the base unit. Exactly one unit must be defined as the base unit.");
            }

            if (extractionStatus.HasMultipleBaseUnits)
            {
                errorTypeCount++;
                exceptionMessageBuilder.AppendLine(" - Two or more of the units defined in the enum, are defined as the base unit. Exactly one unit must be defined as the base unit.");
            }

            exceptionMessageBuilder.AppendLine();
            exceptionMessageBuilder.Append("See the documentation for more information.");

            var exceptionMessage = $"The following {(errorTypeCount > 1 ? "issues were" : "issue was")} encountered while attempting to process the provided unit enum:{Environment.NewLine}{exceptionMessageBuilder.ToString()}";

            throw new InvalidUnitEnumException(exceptionMessage, typeof(TUnit));
        }

        private class EnumUnitExtractionStatus
        {
            public bool HasBaseUnit { get; set; } = false;
            public bool ValueIsMissingBothAttributes { get; set; } = false;
            public bool HasBothAttributes { get; set; } = false;
            public bool IsMissingBaseUnit { get; set; } = false;
            public bool HasMultipleBaseUnits { get; set; } = false;
        }
    }
}