using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            new EnumUnitRepository<TestUnit_Valid>();
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_EnumIsMissingBaseUnitAttribute_THEN_CreateInstance()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                () => new EnumUnitRepository<TestUnit_MissingEnumAttribute>(),
                exception => Assert.AreEqual(typeof(TestUnit_MissingEnumAttribute), exception.EnumType)
            );
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_EnumBaseUnitAttributeIsInvalid_THEN_CreateInstance()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(
                () => new EnumUnitRepository<TestUnit_InvalidBaseUnit>(),
                exception => Assert.AreEqual(typeof(TestUnit_InvalidBaseUnit), exception.EnumType)
            );
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const double expectedConversionValue = 1d;
            var repository = new EnumUnitRepository<TestUnit_Valid>();

            // Act
            var actualConversionValue = repository.GetUnitValueInBaseUnits(TestUnit_Valid.Metre);

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
            var actualConversionValue = repository.GetUnitValueInBaseUnits(TestUnit_BaseUnitHasUnitAttribute.Metre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasUnitAttribute_THEN_ReturnConversionValue()
        {
            // Arrange
            const double expectedConversionValue = 1000d;
            var repository = new EnumUnitRepository<TestUnit_Valid>();

            // Act
            var actualConversionValue = repository.GetUnitValueInBaseUnits(TestUnit_Valid.Millimetre);

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
            var actualConversionValue = repository.GetUnitValueInBaseUnits(TestUnit_MissingUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPerformanceConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasInvalidUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            double? expectedConversionValue = null;
            var repository = new EnumUnitRepository<TestUnit_InvalidUnitValue>();

            // Act
            var actualConversionValue = repository.GetUnitValueInBaseUnits(TestUnit_InvalidUnitValue.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsBaseUnit_UnitHasNoUnitAttribute_THEN_ReturnOne()
        {
            // Arrange
            const decimal expectedConversionValue = 1m;
            var repository = new EnumUnitRepository<TestUnit_Valid>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitValueInBaseUnits(TestUnit_Valid.Metre);

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
            var actualConversionValue = repository.GetPreciseUnitValueInBaseUnits(TestUnit_BaseUnitHasUnitAttribute.Metre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasUnitAttribute_THEN_ReturnConversionValue()
        {
            // Arrange
            const decimal expectedConversionValue = 1000m;
            var repository = new EnumUnitRepository<TestUnit_Valid>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitValueInBaseUnits(TestUnit_Valid.Millimetre);

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
            var actualConversionValue = repository.GetPreciseUnitValueInBaseUnits(TestUnit_MissingUnitAttribute.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_GettingPrecisionConversionValue_WHILE_UnitIsNotBaseUnit_UnitHasInvalidUnitAttribute_THEN_ReturnNull()
        {
            // Arrange
            decimal? expectedConversionValue = null;
            var repository = new EnumUnitRepository<TestUnit_InvalidUnitValue>();

            // Act
            var actualConversionValue = repository.GetPreciseUnitValueInBaseUnits(TestUnit_InvalidUnitValue.Decimetre);

            // Assert
            Assert.AreEqual(expectedConversionValue, actualConversionValue);
        }

        [TestMethod]
        public void WHEN_ReadingAllUnits_WHILE_UnitEnumIsValid_THEN_AllUnitsHasCorrectValue()
        {
            // Arrange
            var unitType = typeof(TestUnit_Valid);
            var baseUnit = System.Enum.Parse(unitType, System.Enum.GetName(unitType, unitType.GetCustomAttribute<UnitEnumAttribute>(false).BaseUnit));

            // Act
            var repository = new EnumUnitRepository<TestUnit_Valid>();

            // Assert
            foreach (var unit in System.Enum.GetValues(typeof(TestUnit_Valid)).OfType<TestUnit_Valid>())
            {
                if (unit.Equals(baseUnit))
                {
                    Assert.AreEqual(1d, repository.GetUnitValueInBaseUnits(unit));
                    Assert.AreEqual(1m, repository.GetPreciseUnitValueInBaseUnits(unit));
                    continue;
                }

                var unitAttribute = unitType.GetField(System.Enum.GetName(unitType, unit)).GetCustomAttribute<UnitAttribute>(false);

                var expectedDoubleValue = double.Parse(unitAttribute.ValueInBaseUnits, NumberStyles.Any, CultureInfo.InvariantCulture);
                var expectedDecimalValue = decimal.Parse(unitAttribute.ValueInBaseUnits, NumberStyles.Any, CultureInfo.InvariantCulture);

                Assert.AreEqual(expectedDoubleValue, repository.GetUnitValueInBaseUnits(unit));
                Assert.AreEqual(expectedDecimalValue, repository.GetPreciseUnitValueInBaseUnits(unit));
            }
        }
    }
}