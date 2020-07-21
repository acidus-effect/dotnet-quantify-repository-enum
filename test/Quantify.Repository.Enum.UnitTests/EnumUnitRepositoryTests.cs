using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Quantify.Repository.Enum.ValueParser;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests
{
    [TestClass]
    public class EnumUnitRepositoryTests
    {
        [TestMethod]
        public void WHILE_ArgumentsAreValid_WHEN_Instantiating_THEN_CreateInstance()
        {
            // Arrange
            var stringValueParserMock = new Mock<StringValueParser<double>>();
            var enumUnitExtractorMock = new Mock<EnumUnitExtractor<double, TestUnit>>(stringValueParserMock.Object);

            enumUnitExtractorMock.Setup(enumUnitExtractor => enumUnitExtractor.Extract()).Returns(new Dictionary<TestUnit, double>());

            // Act
            var enumUnitRepository = new EnumUnitRepository<double, TestUnit>(enumUnitExtractorMock.Object);

            // Assert
            enumUnitExtractorMock.Verify(enumUnitExtractor => enumUnitExtractor.Extract(), Times.Once);
        }

        [TestMethod]
        public void WHILE_ArgumentIsNull_WHEN_Instantiating_THEN_ThrowException()
        {
            // Arrange & Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("enumUnitExtractor", () => new EnumUnitRepository<double, TestUnit>(null));
        }

        [TestMethod]
        public void WHILE_UnitExists_WHEN_GettingUnitData_THEN_ReturnCorrectUnitData()
        {
            // Arrange
            var stringValueParserMock = new Mock<StringValueParser<double>>();
            var enumUnitExtractorMock = new Mock<EnumUnitExtractor<double, TestUnit>>(stringValueParserMock.Object);

            var expectedUnit = TestUnit.Centimetre;
            var expectedUnitValue = 42;
            var testUnitDataDictionary = new Dictionary<TestUnit, double>() { { expectedUnit, expectedUnitValue }, { TestUnit.Metre, 1337 } };

            enumUnitExtractorMock.Setup(enumUnitExtractor => enumUnitExtractor.Extract()).Returns(testUnitDataDictionary);

            var enumUnitRepository = new EnumUnitRepository<double, TestUnit>(enumUnitExtractorMock.Object);

            // Act
            var actualUnitData = enumUnitRepository.GetUnit(expectedUnit);

            // Assert
            Assert.AreEqual(expectedUnit, actualUnitData.Unit);
            Assert.AreEqual(expectedUnitValue, actualUnitData.Value);
        }

        [TestMethod]
        public void WHILE_UnitDoesNotExists_WHEN_GettingUnitData_THEN_ReturnCorrectUnitData()
        {
            // Arrange
            var stringValueParserMock = new Mock<StringValueParser<double>>();
            var enumUnitExtractorMock = new Mock<EnumUnitExtractor<double, TestUnit>>(stringValueParserMock.Object);

            var testUnitDataDictionary = new Dictionary<TestUnit, double>() { { TestUnit.Centimetre, 42 }, { TestUnit.Metre, 1337 } };

            enumUnitExtractorMock.Setup(enumUnitExtractor => enumUnitExtractor.Extract()).Returns(testUnitDataDictionary);

            var enumUnitRepository = new EnumUnitRepository<double, TestUnit>(enumUnitExtractorMock.Object);

            // Act
            var actualUnitData = enumUnitRepository.GetUnit(TestUnit.Kilometre);

            // Assert
            Assert.IsNull(actualUnitData);
        }
    }
}