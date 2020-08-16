using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.ValueParser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Quantify.Repository.Enum.IntegrationTests
{
    [TestClass]
    public class EnumUnitExtractorTests
    {
        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_UnitType_ValidEnum_THEN_ReturnUnitDictionary()
        {
            //Arrange
            var stringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit>(stringValueParser);
            var enumType = typeof(TestUnit);

            var expectedUnitValueDictionary = new Dictionary<TestUnit, double>();

            foreach (TestUnit expectedUnit in System.Enum.GetValues(typeof(TestUnit)))
            {
                var attributes = enumType.GetField(System.Enum.GetName(enumType, expectedUnit)).GetCustomAttributes(false);

                var unitAttribute = attributes.FirstOrDefault(attribute => attribute is UnitAttribute) as UnitAttribute;
                var baseUnitAttribute = attributes.FirstOrDefault(attribute => attribute is BaseUnitAttribute) as BaseUnitAttribute;

                if (unitAttribute != null)
                {
                    expectedUnitValueDictionary.Add(expectedUnit, double.Parse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture));
                }
                else if (baseUnitAttribute != null)
                {
                    expectedUnitValueDictionary.Add(expectedUnit, 1);
                }
            }

            // Act
            var actualUnitValueDictionary = enumUnitExtractor.Extract();

            // Assert
            CollectionAssert.AreEquivalent(expectedUnitValueDictionary.ToList(), actualUnitValueDictionary.ToList());
        }

        [TestMethod]
        public void WHEN_ExtractingUnitEnumValues_WHILE_Enum_Valid_ValueMissingAttribute_THEN_ReturnExtractedUnitDictionary()
        {
            //Arrange
            var stringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MissingAttribute>(stringValueParser);
            var enumType = typeof(TestUnit_MissingAttribute);

            var expectedUnitValueDictionary = new Dictionary<TestUnit_MissingAttribute, double>();

            foreach (TestUnit_MissingAttribute expectedUnit in System.Enum.GetValues(typeof(TestUnit_MissingAttribute)))
            {
                var attributes = enumType.GetField(System.Enum.GetName(enumType, expectedUnit)).GetCustomAttributes(false);

                var unitAttribute = attributes.FirstOrDefault(attribute => attribute is UnitAttribute) as UnitAttribute;
                var baseUnitAttribute = attributes.FirstOrDefault(attribute => attribute is BaseUnitAttribute) as BaseUnitAttribute;

                if (unitAttribute != null)
                {
                    expectedUnitValueDictionary.Add(expectedUnit, double.Parse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture));
                }
                else if (baseUnitAttribute != null)
                {
                    expectedUnitValueDictionary.Add(expectedUnit, 1);
                }
            }

            // Act
            var actualUnitValueDictionary = enumUnitExtractor.Extract();

            // Assert
            CollectionAssert.AreEquivalent(expectedUnitValueDictionary.ToList(), actualUnitValueDictionary.ToList());
        }

        [TestMethod]
        public void WHEN_ExtractingUnitEnumValues_WHILE_Enum_Invalid_ValueHasMultipleAttributes_THEN_ThrowException()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_BaseUnitAlsoUnit>(doubleStringValueParser);

            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(() => enumUnitExtractor.Extract());
        }

        [TestMethod]
        public void WHEN_ExtractingUnitEnumValues_WHILE_Enum_Invalid_HasNoBaseUnit_THEN_ThrowException()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MissingBaseUnit>(doubleStringValueParser);

            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(() => enumUnitExtractor.Extract());
        }

        [TestMethod]
        public void WHEN_ExtractingUnitEnumValues_WHILE_Enum_Invalid_HasMultipleBaseUnits_THEN_ThrowException()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MultipleBaseUnits>(doubleStringValueParser);

            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(() => enumUnitExtractor.Extract());
        }

        [TestMethod]
        public void WHEN_CreatingExtractionReport_WHILE_Enum_Valid_THEN_CreateReportWithoutErrorsAndWarnings()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit>(doubleStringValueParser);

            // Act
            var report = enumUnitExtractor.CreateExtractionReport();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsFalse(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);
        }

        [TestMethod]
        public void WHEN_CreatingExtractionReport_WHILE_Enum_Valid_HasValueMissingAttributes_THEN_CreateReportWithWarnings()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MissingAttribute>(doubleStringValueParser);

            // Act
            var report = enumUnitExtractor.CreateExtractionReport();

            // Assert
            Assert.IsTrue(report.HasWarnings);
            Assert.IsTrue(report.HasValuesMissingAttributesWarning);

            Assert.IsFalse(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);
        }

        [TestMethod]
        public void WHEN_CreatingExtractionReport_WHILE_Enum_Invalid_HasNoBaseUnit_THEN_CreateReportWithErrors()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MissingBaseUnit>(doubleStringValueParser);

            // Act
            var report = enumUnitExtractor.CreateExtractionReport();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsTrue(report.HasErrors);
            Assert.IsTrue(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);
        }

        [TestMethod]
        public void WHEN_CreatingExtractionReport_WHILE_Enum_Invalid_HasMultipleBaseUnits_THEN_CreateReportWithErrors()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MultipleBaseUnits>(doubleStringValueParser);

            // Act
            var report = enumUnitExtractor.CreateExtractionReport();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsTrue(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsTrue(report.HasMultipleBaseUnitsError);
            Assert.IsFalse(report.HasValuesWithBothAttributesError);
        }

        [TestMethod]
        public void WHEN_CreatingExtractionReport_WHILE_Enum_Invalid_HasValueWithMultipleAttributes_THEN_CreateReportWithErrors()
        {
            // Arrange
            var doubleStringValueParser = new StringToDoubleValueParser();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_BaseUnitAlsoUnit>(doubleStringValueParser);

            // Act
            var report = enumUnitExtractor.CreateExtractionReport();

            // Assert
            Assert.IsFalse(report.HasWarnings);
            Assert.IsFalse(report.HasValuesMissingAttributesWarning);

            Assert.IsTrue(report.HasErrors);
            Assert.IsFalse(report.HasNoBaseUnitError);
            Assert.IsFalse(report.HasMultipleBaseUnitsError);
            Assert.IsTrue(report.HasValuesWithBothAttributesError);
        }
    }
}