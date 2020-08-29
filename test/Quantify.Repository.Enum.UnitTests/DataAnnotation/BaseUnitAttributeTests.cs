using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests.DataAnnotation
{
    [TestClass]
    public class BaseUnitAttributeTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentsAreValid_THEN_CreateInstance()
        {
            // Arrange
            const TestUnit expectedBaseUnit = TestUnit.Centimetre;

            // Act
            var attribute = new BaseUnitAttribute(expectedBaseUnit);

            // Assert
            Assert.AreEqual(expectedBaseUnit, attribute.BaseUnit);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("baseUnit", () => new BaseUnitAttribute(null));
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNotEnum_THEN_ThrowException()
        {
            // Arrange
            const string expectedBaseUnit = "Hello, World!";

            // Act & Assert
            ExceptionHelpers.ExpectArgumentException("baseUnit", () => new BaseUnitAttribute(expectedBaseUnit));
        }
    }
}