using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.ValueParser;

namespace Quantify.Repository.Enum.UnitTests.ValueParser
{
    [TestClass]
    public class StringValueParserFactoryTests
    {
        [TestMethod]
        public void WHEN_BuildingValueParser_WHILE_ArgumentTypeDouble_THEN_ReturnDoubleStringValueParser()
        {
            // Arrange
            var stringValueParserFactory = new StringValueParserFactory<double>();

            // Act
            var stringValueParser = stringValueParserFactory.Build();

            // Assert
            Assert.IsInstanceOfType(stringValueParser, typeof(StringToDoubleValueParser));
        }

        [TestMethod]
        public void WHEN_BuildingValueParser_WHILE_ArgumentTypeDecimal_THEN_ReturnDecimalStringValueParser()
        {
            // Arrange
            var stringValueParserFactory = new StringValueParserFactory<decimal>();

            // Act
            var stringValueParser = stringValueParserFactory.Build();

            // Assert
            Assert.IsInstanceOfType(stringValueParser, typeof(StringToDecimalValueParser));
        }

        [TestMethod]
        public void WHEN_BuildingValueParser_WHILE_InvalidArgumentType_THEN_ThrowException()
        {
            // Arrange
            var stringValueParserFactory = new StringValueParserFactory<string>();

            ExceptionHelpers.ExpectException<GenericArgumentException>(
                // Act
                () => stringValueParserFactory.Build(),
                // Assert
                (exception) =>
                {
                    Assert.AreEqual("TValue", exception.ArgumentName);
                    Assert.AreEqual(typeof(string), exception.ArgumentType);
                }
            );
        }
    }
}