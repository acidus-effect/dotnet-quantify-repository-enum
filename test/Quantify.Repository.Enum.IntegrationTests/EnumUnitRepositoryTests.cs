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
        public void WHEN_ReadingAllUnit_WHILE_UnitEnumIsValid_THEN_AllUnitsHasCorrectValue()
        {
            // Act
            var enumUnitRepository = EnumUnitRepository<double, TestUnit>.CreateInstance();

            // Assert
            var enumType = typeof(TestUnit);
            foreach (var unit in System.Enum.GetValues(typeof(TestUnit)).OfType<TestUnit>())
            {
                var attributes = enumType.GetField(System.Enum.GetName(enumType, unit)).GetCustomAttributes(false);

                var unitAttribute = attributes.FirstOrDefault(attribute => attribute is UnitAttribute) as UnitAttribute;
                var baseUnitAttribute = attributes.FirstOrDefault(attribute => attribute is BaseUnitAttribute) as BaseUnitAttribute;

                var unitData = enumUnitRepository.GetUnit(unit);

                if (unitData == null)
                {
                    Assert.IsTrue(false, $"The enum repository doesn't have any unit data for the unit {unit}.");
                    break;
                }

                if (unitAttribute != null && baseUnitAttribute == null)
                {
                    Assert.AreEqual(double.Parse(unitAttribute.ConversionValue, NumberStyles.Any, CultureInfo.InvariantCulture), unitData.Value);
                }
                else if (unitAttribute == null && baseUnitAttribute != null)
                {
                    Assert.AreEqual(1, unitData.Value);
                }
                else if (unitAttribute == null && baseUnitAttribute == null)
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
        public void WHEN_Instantiating_WHILE_UnitIsNotAnEnum_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<GenericArgumentException>(() => EnumUnitRepository<double, int>.CreateInstance());
        }

        [TestMethod]
        public void WHEN_Instantiating_WHILE_UnitEnumHasErrors_THEN_ThrowException()
        {
            // Act & Assert
            ExceptionHelpers.ExpectException<InvalidUnitEnumException>(() => EnumUnitRepository<double, TestUnit_MissingAttribute>.CreateInstance());
        }
    }
}