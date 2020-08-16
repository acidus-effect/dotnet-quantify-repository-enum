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
            new EnumUnitExtractor<double, TestUnit>(stringValueParser);
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
        public void WHEN_Instantiating_WHILE_UnitType_NotEnum_THEN_ThrowException()
        {
            // Arrange
            var stringValueParserMock = new Mock<StringValueParser<double>>();

            // Act & Assert
            ExceptionHelpers.ExpectException<GenericArgumentException>(() => new EnumUnitExtractor<double, int>(stringValueParserMock.Object));
        }
    }
}