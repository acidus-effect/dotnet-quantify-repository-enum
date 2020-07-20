using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.UnitTests.Assets;
using Quantify.Repository.Enum.ValueParser;
using System;
using System.Globalization;

namespace Quantify.Repository.Enum.UnitTests.ValueParser
{
    [TestClass]
    public class StringToDecimalParserTests
    {
        [TestMethod]
        public void WHILE_ArgumentIsNull_WHEN_ParsingValue_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("valueString", () => valueCalculator.Parse(null));
        }

        [TestMethod]
        public void WHILE_StringIsEmpty_WHEN_ParsingValue_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("valueString", () => valueCalculator.Parse(string.Empty));
        }

        [TestMethod]
        public void WHILE_StringContainsOnlyWhitespaces_WHEN_ParsingValue_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("valueString", () => valueCalculator.Parse("   "));
        }

        [TestMethod]
        public void WHILE_StringContainsText_WHEN_ParsingValue_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectException<FormatException>(() => valueCalculator.Parse("Hello, World!"));
        }

        [DataTestMethod]
        [DataRow("23.658")]
        [DataRow("-65.8745")]
        [DataRow("0")]
        public void WHILE_ValueStringIsValid_WHEN_ParsingValue_THEN_ReturnStringValueInDecimalFormat(string valueString)
        {
            // Arrange
            var expectedValue = decimal.Parse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture);

            var valueCalculator = new StringToDecimalValueParser();

            // Act
            var actualValue = valueCalculator.Parse(valueString);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}