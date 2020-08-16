using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;
using System.Collections.Generic;

namespace Quantify.Repository.Enum.UnitTests
{
    [TestClass]
    public class EnumUnitExtractionReportTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentsAreValid_THEN_CreateInstance()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>();
            var enumValuesMarkedAsBaseUnits = new HashSet<string>();
            var enumValuesMissingAttributes = new HashSet<string>();
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>();

            // Act
            var report = new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);

            // Assert
            Assert.AreSame(validUnitEnumValuesDictionary, report.ValidUnitEnumValuesDictionary);
            Assert.AreSame(enumValuesMarkedAsBaseUnits, report.EnumValuesMarkedAsBaseUnits);
            Assert.AreSame(enumValuesMissingAttributes, report.EnumValuesMissingAttributes);
            Assert.AreSame(enumValuesWithBothBaseUnitAndValueAttribute, report.EnumValuesWithBothBaseUnitAndValueAttribute);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>();
            var enumValuesMarkedAsBaseUnits = new HashSet<string>();
            var enumValuesMissingAttributes = new HashSet<string>();
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("validUnitEnumValuesDictionary", () => new EnumUnitExtractionReport<string, string>(null, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute));
            ExceptionHelpers.ExpectArgumentNullException("enumValuesMarkedAsBaseUnits", () => new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, null, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute));
            ExceptionHelpers.ExpectArgumentNullException("enumValuesMissingAttributes", () => new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, null, enumValuesWithBothBaseUnitAndValueAttribute));
            ExceptionHelpers.ExpectArgumentNullException("enumValuesMissingAttributes", () => new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, null));
        }

        [TestMethod]
        public void WHEN_CheckingIfReportHasWarningsOrErrors_WHILE_DataIsValid_THEN_HasNoWarningsOrErrors()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>() { { "Hello", "World" }, { "1337", "4242" } };
            var enumValuesMarkedAsBaseUnits = new HashSet<string>() { "1337" };
            var enumValuesMissingAttributes = new HashSet<string>() { };
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>() { };

            // Act
            var report = new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);
            var reportMessage = report.CreateFormatedWarningsAndErrorsString();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsFalse(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);

            Assert.IsTrue(string.IsNullOrEmpty(reportMessage));
        }

        [TestMethod]
        public void WHEN_CheckingIfReportHasMissingAttributesWarnings_WHILE_HasValuesWithMissingAttributes_THEN_HasWarnings()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>() { { "Hello", "World" }, { "1337", "4242" } };
            var enumValuesMarkedAsBaseUnits = new HashSet<string>() { "1337" };
            var enumValuesMissingAttributes = new HashSet<string>() { "1337" };
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>() { };

            // Act
            var report = new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);
            var reportMessage = report.CreateFormatedWarningsAndErrorsString();

            // Assert
            Assert.IsTrue(report.HasWarnings);
            Assert.IsTrue(report.HasValuesMissingAttributesWarning);

            Assert.IsFalse(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);

            Assert.IsTrue(reportMessage.Contains("The following enum values are not annotated with neither a enum unit attribute nor a enum base unit attribute"));
        }

        [TestMethod]
        public void WHEN_CheckingIfReportHasBaseUnitErrors_WHILE_HasNoBaseUnits_THEN_HasErrors()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>() { { "Hello", "World" }, { "1337", "4242" } };
            var enumValuesMarkedAsBaseUnits = new HashSet<string>() { };
            var enumValuesMissingAttributes = new HashSet<string>() { };
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>() { };

            // Act
            var report = new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);
            var reportMessage = report.CreateFormatedWarningsAndErrorsString();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsTrue(report.HasErrors);
            Assert.IsTrue(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);

            Assert.IsTrue(reportMessage.Contains("No enum value annotated with the base unit attribute was found"));
        }

        [TestMethod]
        public void WHEN_CheckingIfReportHasBaseUnitErrors_WHILE_HasMoreThanOneBaseUnit_THEN_HasErrors()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>() { { "Hello", "World" }, { "1337", "4242" } };
            var enumValuesMarkedAsBaseUnits = new HashSet<string>() { "1337", "4242" };
            var enumValuesMissingAttributes = new HashSet<string>() { };
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>() { };

            // Act
            var report = new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);
            var reportMessage = report.CreateFormatedWarningsAndErrorsString();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsTrue(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsTrue(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);

            Assert.IsTrue(reportMessage.Contains("More than one base unit was found"));
        }

        [TestMethod]
        public void WHEN_CheckingIfReportHasMultipleAttributesErrors_WHILE_HasValuesWithMultipleAttributes_THEN_HasErrors()
        {
            // Arrange
            var validUnitEnumValuesDictionary = new Dictionary<string, string>() { { "Hello", "World" }, { "1337", "4242" } };
            var enumValuesMarkedAsBaseUnits = new HashSet<string>() { "1337" };
            var enumValuesMissingAttributes = new HashSet<string>() { };
            var enumValuesWithBothBaseUnitAndValueAttribute = new HashSet<string>() { "Hallo" };

            // Act
            var report = new EnumUnitExtractionReport<string, string>(validUnitEnumValuesDictionary, enumValuesMarkedAsBaseUnits, enumValuesMissingAttributes, enumValuesWithBothBaseUnitAndValueAttribute);
            var reportMessage = report.CreateFormatedWarningsAndErrorsString();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsTrue(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsTrue(report.HasValuesWithBothAttributesError);

            Assert.IsTrue(reportMessage.Contains("The following enum values are annotated as both a unit and a base unit"));
        }
    }
}