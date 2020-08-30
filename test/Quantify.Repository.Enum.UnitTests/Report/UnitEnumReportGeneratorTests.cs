using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantify.Repository.Enum.Report;
using Quantify.Repository.Enum.Test.Assets;

namespace Quantify.Repository.Enum.UnitTests.Report
{
    [TestClass]
    public class UnitEnumReportGeneratorTests
    {
        [TestMethod]
        public void WHEN_CreatingReport_WHILE_GenericArgumentIsNotAnEnum_THEN_ThrowException()
        {
            // Arrange
            var reportGenerator = new UnitEnumReportGenerator();

            // Act
            ExceptionHelpers.ExpectException<GenericArgumentException>(
                () => reportGenerator.CreateReport<int>(),
                exception =>
                {
                    Assert.AreEqual("TUnit", exception.ArgumentName);
                    Assert.AreEqual(typeof(int), exception.ArgumentType);
                }
            );
        }
    }
}