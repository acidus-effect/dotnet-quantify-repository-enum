using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantify.Repository.Enum
{
    public class EnumUnitExtractionReport<TUnit, TValue>
    {
        public IReadOnlyDictionary<TUnit, TValue> ValidUnitEnumValuesDictionary { get; }
        public ISet<TUnit> EnumValuesMarkedAsBaseUnits { get; }
        public ISet<TUnit> EnumValuesMissingAttributes { get; }
        public ISet<TUnit> EnumValuesWithBothBaseUnitAndValueAttribute { get; }

        public bool HasNoBaseUnitError => EnumValuesMarkedAsBaseUnits.Any() == false;
        public bool HasMultipleBaseUnitsError => EnumValuesMarkedAsBaseUnits.Count() > 1;
        public bool HasValuesMissingAttributesWarning => EnumValuesMissingAttributes.Any();
        public bool HasValuesWithBothAttributesError => EnumValuesWithBothBaseUnitAndValueAttribute.Any();

        public bool HasErrors => HasNoBaseUnitError || HasMultipleBaseUnitsError || HasValuesWithBothAttributesError;
        public bool HasWarnings => HasValuesMissingAttributesWarning == true;

        internal EnumUnitExtractionReport(IReadOnlyDictionary<TUnit, TValue> validUnitEnumValuesDictionary, ISet<TUnit> enumValuesMarkedAsBaseUnits, ISet<TUnit> enumValuesMissingAttributes, ISet<TUnit> enumValuesWithBothBaseUnitAndValueAttribute)
        {
            ValidUnitEnumValuesDictionary = validUnitEnumValuesDictionary ?? throw new ArgumentNullException(nameof(validUnitEnumValuesDictionary));
            EnumValuesMarkedAsBaseUnits = enumValuesMarkedAsBaseUnits ?? throw new ArgumentNullException(nameof(enumValuesMarkedAsBaseUnits));
            EnumValuesMissingAttributes = enumValuesMissingAttributes ?? throw new ArgumentNullException(nameof(enumValuesMissingAttributes));
            EnumValuesWithBothBaseUnitAndValueAttribute = enumValuesWithBothBaseUnitAndValueAttribute ?? throw new ArgumentNullException(nameof(enumValuesMissingAttributes));
        }

        public string CreateFormatedWarningsAndErrorsString()
        {
            var reportStringBuilder = new StringBuilder();

            if (HasWarnings)
            {
                reportStringBuilder.AppendLine(" - The following warnings were found while analyzing the provided unit enum:\n");

                if (HasValuesMissingAttributesWarning)
                    reportStringBuilder.AppendLine($" -- The following enum values are not annotated with neither a enum unit attribute nor a enum base unit attribute: { string.Join(", ", EnumValuesMissingAttributes) }.");

                reportStringBuilder.AppendLine();
            }

            if (HasErrors)
            {
                reportStringBuilder.AppendLine(" - The following errors were found while analyzing the provided unit enum:\n");

                if (HasNoBaseUnitError)
                    reportStringBuilder.AppendLine(" -- No enum value annotated with the base unit attribute was found.");

                if (HasMultipleBaseUnitsError)
                    reportStringBuilder.AppendLine($" -- More than one base unit was found. The following enum values are annotated as base units: { string.Join(", ", EnumValuesMarkedAsBaseUnits) }.");

                if (HasValuesWithBothAttributesError)
                    reportStringBuilder.AppendLine($" -- The following enum values are annotated as both a unit and a base unit: { string.Join(", ", EnumValuesWithBothBaseUnitAndValueAttribute) }.");

            }

            return reportStringBuilder.ToString();
        }
    }
}