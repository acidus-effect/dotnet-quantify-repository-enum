using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests.DataAnnotation
{
    [TestClass]
    public class UnitAttributeTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentsAreValid_THEN_CreateInstance()
        {
            // Arrange
            const string expectedConversionValue = "123456";

            // Act
            var attribute = new UnitAttribute(expectedConversionValue);

            // Assert
            Assert.AreEqual(expectedConversionValue, attribute.ValueInBaseUnits);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("valueInBaseUnits", () => new UnitAttribute(null));
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsEmpty_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("valueInBaseUnits", () => new UnitAttribute(""));
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentContainsOnlyWhitespaces_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("valueInBaseUnits", () => new UnitAttribute("   "));
        }
    }
}