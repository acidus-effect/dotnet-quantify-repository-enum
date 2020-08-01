using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests
{
    [TestClass]
    public class EnumUnitDataTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentsAreValid_THEN_CreateInstance()
        {
            // Arrange
            const TestUnit expectedUnit = TestUnit.Decimetre;
            const int expectedValue = 524;

            // Act
            var enumUnitData = new EnumUnitData<int, TestUnit>(expectedUnit, expectedValue);

            // Assert
            Assert.AreEqual(expectedUnit, enumUnitData.Unit);
            Assert.AreEqual(expectedValue, enumUnitData.Value);
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_ArgumentIsNull_THEN_ThrowException()
        {
            // Arrange
            const string unit = "";
            const string value = "";

            // Act & Assert
            ExceptionHelpers.ExpectArgumentNullException("unit", () => new EnumUnitData<object, object>(null, value));
            ExceptionHelpers.ExpectArgumentNullException("value", () => new EnumUnitData<object, object>(unit, null));
        }
    }
}