using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.ValueParser;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Quantify.Repository.Enum.UnitTests
{
    [TestClass]
    public class EnumUnitExtractorTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentsAreValid_THEN_CreateInstance()
        {
            // Arrange
            var stringValueParser = new Mock<StringValueParser<double>>().Object;

            // Act
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit>(stringValueParser);

            // Assert
            Assert.IsNotNull(enumUnitExtractor);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Arrange
            StringValueParser<double> stringValueParser = null;

            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("stringValueParser", () => new EnumUnitExtractor<double, TestUnit>(stringValueParser));
        }

        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_EnumIsValid_THEN_ReturnUnitDictionary()
        {
            //Arrange
            var stringValueParser = new StringValueParserFactory<double>().Build();
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
            CollectionAssert.AreEquivalent(expectedUnitValueDictionary, (ReadOnlyDictionary<TestUnit, double>) actualUnitValueDictionary);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_UnitTypeArgumentIsNotEnum_THEN_ThrowException()
        {
            // Arrange
            var stringValueParserMock = new Mock<StringValueParser<double>>();

            // Act & Assert
            ExceptionHelpers.ExpectException<GenericArgumentException>(() => new EnumUnitExtractor<double, int>(stringValueParserMock.Object));
        }

        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_UnitEnumHasValueWithoutAttribute_THEN_ThrowException()
        {
            // Arrange
            var stringValueParser = new StringValueParserFactory<double>().Build();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MissingAttribute>(stringValueParser);

            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                // Act
                () => enumUnitExtractor.Extract(),
                // Assert
                exception =>
                {
                    Assert.IsTrue(exception.Message.Contains("The following issue was encountered while attempting to process the provided unit enum:"));
                    Assert.IsTrue(exception.Message.Contains("One or more of the enum values are missing the unit or base unit declaration attribute."));
                }
            );
        }

        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_UnitEnumHasNoBaseUnit_THEN_ThrowException()
        {
            // Arrange
            var stringValueParser = new StringValueParserFactory<double>().Build();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MissingBaseUnit>(stringValueParser);

            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                // Act
                () => enumUnitExtractor.Extract(),
                // Assert
                exception =>
                {
                    Assert.IsTrue(exception.Message.Contains("The following issue was encountered while attempting to process the provided unit enum:"));
                    Assert.IsTrue(exception.Message.Contains("Non of the units defined in the enum, are defined as the base unit. Exactly one unit must be defined as the base unit."));
                }
            );
        }

        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_UnitEnumHasMultipleBaseUnits_THEN_ThrowException()
        {
            // Arrange
            var stringValueParser = new StringValueParserFactory<double>().Build();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MultipleBaseUnits>(stringValueParser);

            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                // Act
                () => enumUnitExtractor.Extract(),
                // Assert
                exception =>
                {
                    Assert.IsTrue(exception.Message.Contains("The following issue was encountered while attempting to process the provided unit enum:"));
                    Assert.IsTrue(exception.Message.Contains("Two or more of the units defined in the enum, are defined as the base unit. Exactly one unit must be defined as the base unit."));
                }
            );
        }

        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_UnitEnumHasValueDefinedAsBothUnitAndBaseUnit_THEN_ThrowException()
        {
            // Arrange
            var stringValueParser = new StringValueParserFactory<double>().Build();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_BaseUnitAlsoUnit>(stringValueParser);

            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                // Act
                () => enumUnitExtractor.Extract(),
                // Assert
                exception =>
                {
                    Assert.IsTrue(exception.Message.Contains("The following issue was encountered while attempting to process the provided unit enum:"));
                    Assert.IsTrue(exception.Message.Contains("One or more of the units defined in the enum, are defined as both a unit and a base unit."));
                }
            );
        }

        [TestMethod]
        public void WHEN_ExtractingValues_WHILE_UnitEnumHasMultipleIssues_THEN_ThrowException()
        {
            // Arrange
            var stringValueParser = new StringValueParserFactory<double>().Build();
            var enumUnitExtractor = new EnumUnitExtractor<double, TestUnit_MultiIssues>(stringValueParser);

            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                // Act
                () => enumUnitExtractor.Extract(),
                // Assert
                exception =>
                {
                    Assert.IsTrue(exception.Message.Contains("The following issues were encountered while attempting to process the provided unit enum:"));
                }
            );
        }
    }
}