namespace AutomaticTelephoneStation.BillingSystem.TariffPlans
{
    public interface ITariffPlan
    {
        public string Name { get; }
        public decimal Fee { get; }
    }
}