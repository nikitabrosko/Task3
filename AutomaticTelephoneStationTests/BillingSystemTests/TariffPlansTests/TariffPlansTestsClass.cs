using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.BillingSystemTests.TariffPlansTests
{
    [TestClass]
    public class TariffPlansTestsClass
    {
        [TestMethod]
        public void TestCreatingLowTariffPlanClass()
        {
            var name = "Low tariff plan";
            var fee = 5M;

            var lowTariffPlanObject = new LowTariffPlan();

            Assert.IsTrue(lowTariffPlanObject.Name.Equals(name)
                          && lowTariffPlanObject.Fee.Equals(fee));
        }

        [TestMethod]
        public void TestCreatingMediumTariffPlanClass()
        {
            var name = "Medium tariff plan";
            var fee = 10M;

            var mediumTariffPlanObject = new MediumTariffPlan();

            Assert.IsTrue(mediumTariffPlanObject.Name.Equals(name)
                          && mediumTariffPlanObject.Fee.Equals(fee));
        }

        [TestMethod]
        public void TestCreatingHighTariffPlanClass()
        {
            var name = "High tariff plan";
            var fee = 15M;

            var highTariffPlanObject = new HighTariffPlan();

            Assert.IsTrue(highTariffPlanObject.Name.Equals(name)
                          && highTariffPlanObject.Fee.Equals(fee));
        }
    }
}