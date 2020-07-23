using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.DataAnnotation;
using Quantify.Repository.Enum.Test.Assets;
using System.Globalization;
using System.Linq;

namespace Quantify.Repository.Enum.IntegrationTests
{
    [TestClass]
    public class EnumUnitRepositoryTests
    {
        [TestMethod]
        public void WHILE_UnitEnumIsValid_WHEN_ReadingAllUnit_THEN_AllUnitsHasCorrectValue()
        {
            // Act
            var enumUnitRepository = EnumUnitRepository<double, TestUnit>.CreateInstance();

            // Assert
            var enumType = typeof(TestUnit);
            foreach (var unit in System.Enum.GetValues(typeof(TestUnit)).OfType<TestUnit>())
            {
                var attributes = enumType.GetField(System.Enum.GetName(enumType, unit)).GetCustomAttributes(false);
                var quantityUnit = attributes.FirstOrDefault(attribute => attribute is QuantityUnitAttribute) as QuantityUnitAttribute;
                var quantityBaseUnit = attributes.FirstOrDefault(attribute => attribute is QuantityBaseUnitAttribute) as QuantityBaseUnitAttribute;

                var unitData = enumUnitRepository.GetUnit(unit);

                if (unitData == null)
                {
                    Assert.IsTrue(false, $"The enum repository doesn't have any unit data for the unit {unit}.");
                    break;
                }

                if (quantityUnit != null && quantityBaseUnit == null)
                {
                    Assert.AreEqual(double.Parse(quantityUnit.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture), unitData.Value);
                }
                else if (quantityUnit == null && quantityBaseUnit != null)
                {
                    Assert.AreEqual(1, unitData.Value);
                }
                else if (quantityUnit == null && quantityBaseUnit == null)
                {
                    Assert.IsTrue(false, $"The enum unit {unit} doesn't have neither a unit value nor a base unit marker.");
                    break;
                }
                else
                {
                    Assert.IsTrue(false, $"The enum unit {unit} has both a unit value and a base unit marker.");
                    break;
                }
            }
        }

        [TestMethod]
        public void WHILE_UnitIsNotAnEnum_WHEN_Instantiating_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<GenericArgumentException>(() => EnumUnitRepository<double, int>.CreateInstance());
        }

        [TestMethod]
        public void WHILE_UnitEnumHasErrors_WHEN_Instantiating_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(() => EnumUnitRepository<double, TestUnit_MissingAttribute>.CreateInstance());
        }
    }
}