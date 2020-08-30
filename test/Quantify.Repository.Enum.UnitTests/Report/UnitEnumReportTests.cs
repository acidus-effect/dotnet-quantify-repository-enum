using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Report;

namespace Quantify.Repository.Enum.UnitTests.Report
{
    [TestClass]
    public class UnitEnumReportTests
    {
        [DataTestMethod]
        [DataRow(false, false, false, false, false, false, false, 0, 0)]
        [DataRow(true, false, false, false, false, true, false, 1, 0)]
        [DataRow(false, true, false, false, false, true, false, 1, 0)]
        [DataRow(false, false, true, false, false, true, false, 1, 0)]
        [DataRow(false, false, false, true, false, false, true, 0, 1)]
        [DataRow(false, false, false, false, true, false, true, 0, 1)]
        [DataRow(false, false, true, false, true, true, true, 1, 1)]
        [DataRow(true, true, true, true, true, true, true, 3, 2)]
        public void WHEN_Instantiating_WHILE_ArgumentsAreSet_THEN_CreateInstance(
            bool expectedHasValueMissingUnitAttribute,
            bool expectedHasValueWithInvalidUnitAttribute,
            bool expectedBaseUnitHasUnitAttribute,
            bool expectedIsMissingBaseUnitAttribute,
            bool expectedHasInvalidBaseUnitAttribute,
            bool expectedHasWarnings,
            bool expectedHasErrors,
            int expectedNumberOfWarnings,
            int expectedNumberOfErrors)
        {
            // Act
            var report = new UnitEnumReport(expectedHasValueMissingUnitAttribute, expectedHasValueWithInvalidUnitAttribute, expectedBaseUnitHasUnitAttribute, expectedIsMissingBaseUnitAttribute, expectedHasInvalidBaseUnitAttribute);

            // Assert
            Assert.AreEqual(expectedHasValueMissingUnitAttribute, report.HasValueMissingUnitAttribute);
            Assert.AreEqual(expectedHasValueWithInvalidUnitAttribute, report.HasValueWithInvalidUnitAttribute);
            Assert.AreEqual(expectedBaseUnitHasUnitAttribute, report.BaseUnitHasUnitAttribute);
            Assert.AreEqual(expectedIsMissingBaseUnitAttribute, report.IsMissingBaseUnitAttribute);
            Assert.AreEqual(expectedHasInvalidBaseUnitAttribute, report.HasInvalidBaseUnitAttribute);

            Assert.AreEqual(expectedHasWarnings, report.HasWarnings);
            Assert.AreEqual(expectedHasErrors, report.HasErrors);

            Assert.IsNotNull(report.Warnings);
            Assert.IsNotNull(report.Errors);

            Assert.AreEqual(expectedNumberOfWarnings, report.Warnings.Count);
            Assert.AreEqual(expectedNumberOfErrors, report.Errors.Count);
        }
    }
}