using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Report;
using Quantify.Repository.Enum.Test.Assets;
using Quantify.Repository.Enum.Test.Assets.TestUnits;

namespace Quantify.Repository.Enum.IntegrationTests.Report
{
    [TestClass]
    public class UnitEnumReportGeneratorTests
    {
        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_Valid_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit>();

            // Assert
            Assert.AreEqual(false, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(false, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(false, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(false, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(false, report.HasInvalidBaseUnitAttribute);
        }

        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_MissingBaseUnit_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit_MissingBaseUnit>();

            // Assert
            Assert.AreEqual(false, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(false, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(false, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(true, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(false, report.HasInvalidBaseUnitAttribute);
        }

        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_InvalidBaseUnit_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit_InvalidBaseUnitValue>();

            // Assert
            Assert.AreEqual(false, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(false, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(false, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(false, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(true, report.HasInvalidBaseUnitAttribute);
        }

        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_BaseUnitIsAlsoUnit_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit_BaseUnitHasUnitAttribute>();

            // Assert
            Assert.AreEqual(false, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(false, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(true, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(false, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(false, report.HasInvalidBaseUnitAttribute);
        }

        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_ValueIsMissingUnitAttribute_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit_MissingUnitAttribute>();

            // Assert
            Assert.AreEqual(true, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(false, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(false, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(false, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(false, report.HasInvalidBaseUnitAttribute);
        }

        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_ValueHasInvalidUnitAttribute_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit_InvalidUnitAttribute>();

            // Assert
            Assert.AreEqual(false, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(true, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(false, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(false, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(false, report.HasInvalidBaseUnitAttribute);
        }

        [TestMethod]
        public void WHEN_CreatingReport_WHILE_Enum_MultipleValuesWithMissingAndInvalidUnitAttributes_THEN_CreateReport()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            var report = reportGenerator.CreateReport<TestUnit_MultipleMissingAndInvalidUnitAttributes>();

            // Assert
            Assert.AreEqual(true, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(true, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(false, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(false, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(false, report.HasInvalidBaseUnitAttribute);
        }
    }
}