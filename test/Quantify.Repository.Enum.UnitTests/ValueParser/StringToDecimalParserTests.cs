using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.ValueParser;
using System;
using System.Globalization;

namespace Quantify.Repository.Enum.UnitTests.ValueParser
{
    [TestClass]
    public class StringToDecimalParserTests
    {
        [TestMethod]
        public void WHEN_ParsingValue_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("valueString", () => valueCalculator.Parse(null));
        }

        [TestMethod]
        public void WHEN_ParsingValue_WHILE_StringIsEmpty_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("valueString", () => valueCalculator.Parse(string.Empty));
        }

        [TestMethod]
        public void WHEN_ParsingValue_WHILE_StringContainsOnlyWhitespaces_THEN_ThrowException()
        {
            // Arrange
            var valueCalculator = new StringToDecimalValueParser();

            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("valueString", () => valueCalculator.Parse("   "));
        }

        [TestMethod]
        public void WHEN_ParsingValue_WHILE_StringContainsText_THEN_ThrowException()
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
        public void WHEN_ParsingValue_WHILE_ValueStringIsValid_THEN_ReturnStringValueInDecimalFormat(string valueString)
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