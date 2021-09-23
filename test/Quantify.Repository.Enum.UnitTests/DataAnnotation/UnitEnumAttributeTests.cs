using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests.DataAnnotation
{
    [TestClass]
    public class UnitEnumAttributeTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentsAreValid_THEN_CreateInstance()
        {
            // Arrange
            const TestUnit_Valid expectedBaseUnit = TestUnit_Valid.Centimetre;

            // Act
            var attribute = new UnitEnumAttribute(expectedBaseUnit);

            // Assert
            Assert.AreEqual(expectedBaseUnit, attribute.BaseUnit);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("baseUnit", () => new UnitEnumAttribute(null));
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNotEnum_THEN_ThrowException()
        {
            // Arrange
            const string expectedBaseUnit = "Hello, World!";

            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("baseUnit", () => new UnitEnumAttribute(expectedBaseUnit));
        }
    }
}