using Quantify.Repository.Enum.DataAnnotation;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantify.Repository.Enum.Report
{
    /// <summary>
    /// Report class containing the result of an analysis of a unit enum type.
    /// </summary>
    public sealed class UnitEnumReport
    {
        /// <summary>
        /// If true, then one or more of the unit enum values are missing the <see cref="UnitAttribute"/> attribute.
        /// </summary>
        /// <remarks>
        /// The fact that a unit enum value is missing the <see cref="UnitAttribute"/> attribute is not considered a fatal error and the quantity will continue to function, while ignoring values that are missing the <see cref="UnitAttribute"/> attribute.
        /// </remarks>
        public bool HasValueMissingUnitAttribute { get; }

        /// <summary>
        /// If true, then one or more of the unit enum values have a <see cref="UnitAttribute"/> attribute with an invalid conversion value. See the documentation for more details.
        /// </summary>
        /// <remarks>
        /// The fact that a unit enum value has an invalid <see cref="UnitAttribute"/> attribute is not considered a fatal error and the quantity will continue to function, while ignoring values with invalid <see cref="UnitAttribute"/> attributes.
        /// </remarks>
        public bool HasValueWithInvalidUnitAttribute { get; }

        /// <summary>
        /// If true, then the unit enum value designated as the unit base value is also annotated with a <see cref="UnitAttribute"/> attribute.
        /// </summary>
        /// <remarks>
        /// The fact that the unit enum value designated as the unit base value is also annotated with a <see cref="UnitAttribute"/> attribute is not considered a fatal error and the quantity will continue to function. The conversion value of the <see cref="UnitAttribute"/> attribute is ignored, since the base unit always has a conversion value of one (1).
        /// </remarks>
        public bool BaseUnitHasUnitAttribute { get; }

        /// <summary>
        /// If true, then the unit enum is missing the <see cref="BaseUnitAttribute"/> attribute.
        /// </summary>
        /// <remarks>
        /// The fact that the unit enum is missing the <see cref="BaseUnitAttribute"/> attribute is considered a fatal error. The instantiation of <see cref="EnumUnitRepository{TUnit}" /> will fail in this case.
        /// </remarks>
        public bool IsMissingBaseUnitAttribute { get; }

        /// <summary>
        /// If true, then the unit enum is annotated with a <see cref="BaseUnitAttribute"/> attribute with an invalid base unit value. See the documentation for more details.
        /// </summary>
        /// <remarks>
        /// The fact that the unit enum is annotated with an invalid <see cref="BaseUnitAttribute"/> attribute is considered a fatal error. The instantiation of <see cref="EnumUnitRepository{TUnit}" /> will fail in this case.
        /// </remarks>
        public bool HasInvalidBaseUnitAttribute { get; }

        /// <summary>
        /// A list of warnings related to the unit enum analyzed.
        /// </summary>
        public IReadOnlyCollection<string> Warnings { get; }

        /// <summary>
        /// A list of errors related to the unit enum analyzed.
        /// </summary>
        public IReadOnlyCollection<string> Errors { get; }

        /// <summary>
        /// Indicates whether or not the unit enum analysis report contains any warnings.
        /// </summary>
        public bool HasWarnings => Warnings.Any();

        /// <summary>
        /// Indicates whether or not the unit enum analysis report contains any errors.
        /// </summary>
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
                warnings.Add("One or more of the enum values have an invalid conversion value defined in its unit attribute. These values will be ignored and will not be available when creating a quantity and when converting a quantity. Please use only numbers to define conversion values. Use a period as decimal separator and commas as thousands separator.");

            if (baseUnitHasUnitAttribute)
                warnings.Add("The base unit is annotated with a unit attribute. This attribute will be ignored, since the base unit always has a conversion value of 1.");

            if (isMissingBaseUnitAttribute)
                errors.Add("The unit enum is missing the base unit attribute.");

            if (hasInvalidBaseUnitAttribute)
                errors.Add("The value of the base unit attribute is not valid. Please provide the value of the current enum, that will function as the base unit.");

            Warnings = warnings.AsReadOnly();
            Errors = errors.AsReadOnly();
        }

        /// <summary>
        /// Creates a comprehensive summary of the report, with all warnings and/or errors.
        /// </summary>
        /// <returns>The report summary as a string.</returns>
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