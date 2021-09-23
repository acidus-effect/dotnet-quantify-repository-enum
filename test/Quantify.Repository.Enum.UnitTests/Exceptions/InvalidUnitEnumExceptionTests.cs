using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Quantify.Repository.Enum.UnitTests.Exceptions
{
    [TestClass]
    public class InvalidUnitEnumExceptionTests
    {
        [TestMethod]
        public void WHEN_Instantiating_Message_ArgumentType_WHILE_ArgumentTypeIsNull_THEN_ArgumentTypeIsNull()
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
    }
}