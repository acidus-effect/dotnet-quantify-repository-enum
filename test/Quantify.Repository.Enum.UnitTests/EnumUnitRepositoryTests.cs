using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Test.Assets;
using System.Globalization;
using System.Linq;
using System.Reflection;

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
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const double expectedConversionValue = 1d;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionValue = repository.GetUnitConversionValue(TestUnit.Metre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsBaseUnit_UnitHasUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const double expectedConversionValue = 1d;
            var repository = new EnumUnitRepository<TestUnit_BaseUnitHasUnitAttribute>();

            // Act
            var actualConversionValue = repository.GetUnitConversionValue(TestUnit_BaseUnitHasUnitAttribute.Metre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasUnitAttribute_THEN_ReturnConversionValue()
        {
            // Arrange
            const double expectedConversionValue = 0.001d;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionValue = repository.GetUnitConversionValue(TestUnit.Millimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            double? expectedConversionValue = null;
            var repository = new EnumUnitRepository<TestUnit_MissingUnitAttribute>();

            // Act
            var actualConversionValue = repository.GetUnitConversionValue(TestUnit_MissingUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasInvalidUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            double? expectedConversionValue = null;
            var repository = new EnumUnitRepository<TestUnit_InvalidUnitAttribute>();

            // Act
            var actualConversionValue = repository.GetUnitConversionValue(TestUnit_InvalidUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const decimal expectedConversionValue = 1m;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitConversionValue(TestUnit.Metre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsBaseUnit_UnitHasUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const decimal expectedConversionValue = 1m;
            var repository = new EnumUnitRepository<TestUnit_BaseUnitHasUnitAttribute>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitConversionValue(TestUnit_BaseUnitHasUnitAttribute.Metre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasUnitAttribute_THEN_ReturnConversionValue()
        {
            // Arrange
            const decimal expectedConversionValue = 0.001m;
            var repository = new EnumUnitRepository<TestUnit>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitConversionValue(TestUnit.Millimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            decimal? expectedConversionValue = null;
            var repository = new EnumUnitRepository<TestUnit_MissingUnitAttribute>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitConversionValue(TestUnit_MissingUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasInvalidUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            decimal? expectedConversionValue = null;
            var repository = new EnumUnitRepository<TestUnit_InvalidUnitAttribute>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitConversionValue(TestUnit_InvalidUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_ReadingAllUnits_WHILE_UnitEnumIsValid_THEN_AllUnitsHasCorrectValue()
        {
            // Arrange
            var unitType = typeof(TestUnit);
            var baseUnit = System.Enum.Parse(unitType, System.Enum.GetName(unitType, unitType.GetCustomAttribute<BaseUnitAttribute>(false).BaseUnit));

            // Act
            var repository = new EnumUnitRepository<TestUnit>();

            // Assert
            foreach (var unit in System.Enum.GetValues(typeof(TestUnit)).OfType<TestUnit>())
            {
                if (unit.Equals(baseUnit))
                {
                    Assert.AreEqual(1d, repository.GetUnitConversionValue(unit));
                    Assert.AreEqual(1m, repository.GetPreciseUnitConversionValue(unit));
                    continue;
                }

                var unitAttribute = unitType.GetField(System.Enum.GetName(unitType, unit)).GetCustomAttribute<UnitAttribute>(false);

                var expectedDoubleValue = double.Parse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture);
                var expectedDecimalValue = decimal.Parse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture);

                Assert.AreEqual(expectedDoubleValue, repository.GetUnitConversionValue(unit));
                Assert.AreEqual(expectedDecimalValue, repository.GetPreciseUnitConversionValue(unit));
            }
        }
    }
}