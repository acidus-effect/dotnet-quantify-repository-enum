using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.Validators;

namespace Quantify.Repository.Enum.UnitTests.Validators
{
    [TestClass]
    public class GenericEnumParametersValidatorTests
    {
        [TestMethod]
        public void WHEN_ValidatingIfArgumentIsOfTypeEnum_WHILE_TypeIsEnum_THEN_ReturnTrue()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<TestUnit>();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WHEN_ValidatingIfArgumentIsOfTypeEnum_WHILE_TypeIsInt_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<int>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WHEN_ValidatingIfArgumentIsOfTypeEnum_WHILE_TypeIsLong_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<long>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WHEN_ValidatingIfArgumentIsOfTypeEnum_WHILE_TypeIsDecimal_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<decimal>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WHEN_ValidatingIfArgumentIsOfTypeEnum_WHILE_TypeIsDouble_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<double>();

            // Assert
            Assert.IsFalse(result);
        }
    }
}