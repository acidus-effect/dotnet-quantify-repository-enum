using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Quantify.Repository.Enum.UnitTests.Exceptions
{
    [TestClass]
    public class InvalidUnitEnumExceptionTests
    {
        [TestMethod]
        public void WHILE_MessageIsNull_WHEN_Instantiating_Message_ArgumentType_THEN_HasDefaultMessage()
        {
            // Arrange
            const string message = null;

            const string expectedMessage = "The unit enum is invalid.";
            Type expectedArgumentType = typeof(InvalidUnitEnumException);

            // Act
            var exception = new InvalidUnitEnumException(message, expectedArgumentType);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreSame(expectedArgumentType, exception.EnumType);
        }

        [TestMethod]
        public void WHILE_ArgumentTypeIsNull_WHEN_Instantiating_Message_ArgumentType_THEN_ArgumentTypeIsNull()
        {
            // Arrange
            const string expectedMessage = "The unit enum is invalid.";
            Type argumentType = null;

            // Act
            var exception = new InvalidUnitEnumException(expectedMessage, argumentType);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.IsNull(exception.EnumType);
        }

        [TestMethod]
        public void WHILE_MessageIsNull_WHEN_Instantiating_Message_ArgumentType_InnerException_THEN_HasDefaultMessage()
        {
            // Arrange
            const string message = null;
            const string expectedMessage = "The unit enum is invalid.";
            Type expectedArgumentType = typeof(InvalidUnitEnumException);
            Exception expectedInnerException = new ArgumentException();

            // Act
            var exception = new InvalidUnitEnumException(message, expectedArgumentType, expectedInnerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreSame(expectedArgumentType, exception.EnumType);
            Assert.AreSame(expectedInnerException, exception.InnerException);
        }

        [TestMethod]
        public void WHILE_ArgumentTypeIsNull_WHEN_Instantiating_Message_ArgumentType_InnerException_THEN_ArgumentTypeIsNull()
        {
            // Arrange
            const string expectedMessage = "The unit enum is invalid.";
            Type expectedArgumentType = null;
            Exception expectedInnerException = new ArgumentException();

            // Act
            var exception = new InvalidUnitEnumException(expectedMessage, expectedArgumentType, expectedInnerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.IsNull(exception.EnumType);
            Assert.AreSame(expectedInnerException, exception.InnerException);
        }

        [TestMethod]
        public void WHILE_InnerExceptionIsNull_WHEN_Instantiating_Message_ArgumentType_InnerException_THEN_InnerExceptionIsNull()
        {
            // Arrange
            const string expectedMessage = "The unit enum is invalid.";
            Type expectedArgumentType = typeof(InvalidUnitEnumException);
            Exception expectedInnerException = null;

            // Act
            var exception = new InvalidUnitEnumException(expectedMessage, expectedArgumentType, expectedInnerException);

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
            Assert.AreSame(expectedArgumentType, exception.EnumType);
            Assert.IsNull(exception.InnerException);
        }
    }
}