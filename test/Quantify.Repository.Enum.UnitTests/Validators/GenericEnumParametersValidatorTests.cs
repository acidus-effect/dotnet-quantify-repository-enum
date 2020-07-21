using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.Validators;

namespace Quantify.Repository.Enum.UnitTests.Validators
{
    [TestClass]
    public class GenericEnumParametersValidatorTests
    {
        [TestMethod]
        public void WHILE_TypeIsEnum_WHEN_ValidatingIfArgumentIsOfTypeEnum_THEN_ReturnTrue()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<TestUnit>();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WHILE_TypeIsInt_WHEN_ValidatingIfArgumentIsOfTypeEnum_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<int>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WHILE_TypeIsLong_WHEN_ValidatingIfArgumentIsOfTypeEnum_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<long>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WHILE_TypeIsDecimal_WHEN_ValidatingIfArgumentIsOfTypeEnum_THEN_ReturnFalse()
        {
            // Arrange
            var genericEnumParametersValidator = new GenericEnumParametersValidator();

            // Act
            var result = genericEnumParametersValidator.GenericParameterIsEnumType<decimal>();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WHILE_TypeIsDouble_WHEN_ValidatingIfArgumentIsOfTypeEnum_THEN_ReturnFalse()
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