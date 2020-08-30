using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantify.Repository.Enum.Report
{
    public sealed class UnitEnumReport
    {
        public bool HasValueMissingUnitAttribute { get; }
        public bool HasValueWithInvalidUnitAttribute { get; }
        public bool BaseUnitHasUnitAttribute { get; }

        public bool IsMissingBaseUnitAttribute { get; }
        public bool HasInvalidBaseUnitAttribute { get; }

        public IReadOnlyCollection<string> Warnings { get; }
        public IReadOnlyCollection<string> Errors { get; }

        public bool HasWarnings => Warnings.Any();
        public bool HasErrors => Errors.Any();

        internal UnitEnumReport(bool hasValueMissingUnitAttribute, bool hasValueWithInvalidUnitAttribute, bool baseUnitHasUnitAttribute, bool isMissingBaseUnitAttribute, bool hasInvalidBaseUnitAttribute)
        {
            HasValueMissingUnitAttribute = hasValueMissingUnitAttribute;
            HasValueWithInvalidUnitAttribute = hasValueWithInvalidUnitAttribute;
            BaseUnitHasUnitAttribute = baseUnitHasUnitAttribute;
            IsMissingBaseUnitAttribute = isMissingBaseUnitAttribute;
            HasInvalidBaseUnitAttribute = hasInvalidBaseUnitAttribute;

            var warnings = new List<string>();
            var errors = new List<string>();

            if (hasValueMissingUnitAttribute)
                warnings.Add("One or more of the enum values are missing the unit attribute. These values will be ignored and will not be available when creating a quantity and when converting a quantity.");

            if (hasValueWithInvalidUnitAttribute)
                warnings.Add("One or more of the enum values have an invalid conversion rate defined in its unit attribute. These values will be ignored and will not be available when creating a quantity and when converting a quantity. Please use only numbers to define conversion rates. Use a period as decimal separator and commas as thousands separator.");

            if (baseUnitHasUnitAttribute)
                warnings.Add("The base unit is annotated with a unit attribute. This attribute will be ignored, since the base unit always has a conversion rate value of 1.");

            if (isMissingBaseUnitAttribute)
                errors.Add("The unit enum is missing the base unit attribute.");

            if (hasInvalidBaseUnitAttribute)
                errors.Add("The value of the base unit attribute is not valid. Please provide the value of the current enum, that will function as the base unit.");

            Warnings = warnings.AsReadOnly();
            Errors = errors.AsReadOnly();
        }

        public string CreateSummary()
        {
            if (HasWarnings == false && HasErrors == false)
            {
                return "No issues were found while analyzing the unit enum :)";
            }

            var summaryStringBuilder = new StringBuilder();

            if (HasWarnings)
            {
                summaryStringBuilder.AppendLine("The following warnings were found while analyzing the provided unit enum:");
                summaryStringBuilder.AppendLine();

                foreach (var warning in Warnings)
                    summaryStringBuilder.AppendLine($" - {warning}");
            }

            if (HasWarnings && HasErrors)
                summaryStringBuilder.AppendLine();

            if (HasErrors)
            {
                summaryStringBuilder.AppendLine("The following errors were found while analyzing the provided unit enum:");
                summaryStringBuilder.AppendLine();

                foreach (var error in Errors)
                    summaryStringBuilder.AppendLine($" - {error}");
            }

            summaryStringBuilder.AppendLine();
            summaryStringBuilder.AppendLine("Please refer to the documentation for more information.");

            return summaryStringBuilder.ToString();
        }
    }
}