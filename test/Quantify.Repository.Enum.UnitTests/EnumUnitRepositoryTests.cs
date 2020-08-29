using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests
{
    [TestClass]
    public class EnumUnitRepositoryTests
    {
        [TestMethod]
        public void WHEN_Instantiating_WHILE_TypeArgumentIsNotEnum_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<GenericArgumentException>(
                () => new EnumUnitRepository<int>(),
                exception =>
                {
                    Assert.AreEqual(typeof(int), exception.ArgumentType);
                    Assert.AreEqual("TUnit", exception.ArgumentName);
                }
            );
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_EnumIsValid_THEN_CreateInstance()
        {
            // Act
            new EnumUnitRepository<TestUnit>();
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_EnumIsMissingBaseUnitAttribute_THEN_CreateInstance()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                () => new EnumUnitRepository<TestUnit_MissingBaseUnit>(),
                exception => Assert.AreEqual(typeof(TestUnit_MissingBaseUnit), exception.EnumType)
            );
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_EnumBaseUnitAttributeIsInvalid_THEN_CreateInstance()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                () => new EnumUnitRepository<TestUnit_InvalidBaseUnitValue>(),
                exception => Assert.AreEqual(typeof(TestUnit_InvalidBaseUnitValue), exception.EnumType)
            );
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionRate_WHILE_UnitIsBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const double expectedConversionRate = 1d;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionRate = repository.GetUnitConversionRate(TestUnit.Metre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionRate_WHILE_UnitIsBaseUnit_UnitHasUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const double expectedConversionRate = 1d;
            var repository = new EnumUnitRepository<TestUnit_BaseUnitHasUnitAttribute>();

            // Act
            var actualConversionRate = repository.GetUnitConversionRate(TestUnit_BaseUnitHasUnitAttribute.Metre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionRate_WHILE_UnitIsNotBaseUnit_UnitHasUnitAttribute_THEN_ReturnConversionRate()
        {
            // Arrange
            const double expectedConversionRate = 0.001d;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionRate = repository.GetUnitConversionRate(TestUnit.Millimetre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionRate_WHILE_UnitIsNotBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            double? expectedConversionRate = null;
            var repository = new EnumUnitRepository<TestUnit_MissingUnitAttribute>();

            // Act
            var actualConversionRate = repository.GetUnitConversionRate(TestUnit_MissingUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionRate_WHILE_UnitIsNotBaseUnit_UnitHasInvalidUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            double? expectedConversionRate = null;
            var repository = new EnumUnitRepository<TestUnit_InvalidUnitAttribute>();

            // Act
            var actualConversionRate = repository.GetUnitConversionRate(TestUnit_InvalidUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionRate_WHILE_UnitIsBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const decimal expectedConversionRate = 1m;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionRate = repository.GetPreciseUnitConversionRate(TestUnit.Metre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionRate_WHILE_UnitIsBaseUnit_UnitHasUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const decimal expectedConversionRate = 1m;
            var repository = new EnumUnitRepository<TestUnit_BaseUnitHasUnitAttribute>();

            // Act
            var actualConversionRate = repository.GetPreciseUnitConversionRate(TestUnit_BaseUnitHasUnitAttribute.Metre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionRate_WHILE_UnitIsNotBaseUnit_UnitHasUnitAttribute_THEN_ReturnConversionRate()
        {
            // Arrange
            const decimal expectedConversionRate = 0.001m;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionRate = repository.GetPreciseUnitConversionRate(TestUnit.Millimetre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionRate_WHILE_UnitIsNotBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            decimal? expectedConversionRate = null;
            var repository = new EnumUnitRepository<TestUnit_MissingUnitAttribute>();

            // Act
            var actualConversionRate = repository.GetPreciseUnitConversionRate(TestUnit_MissingUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionRate_WHILE_UnitIsNotBaseUnit_UnitHasInvalidUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            decimal? expectedConversionRate = null;
            var repository = new EnumUnitRepository<TestUnit_InvalidUnitAttribute>();

            // Act
            var actualConversionRate = repository.GetPreciseUnitConversionRate(TestUnit_InvalidUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionRate, actualConversionRate);
        }
    }
}