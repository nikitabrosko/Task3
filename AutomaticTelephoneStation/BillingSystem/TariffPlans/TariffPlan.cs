namespace AutomaticTelephoneStation.BillingSystem.TariffPlans
{
    public abstract class TariffPlan : ITariffPlan
    {
        public string Name { get; protected set; }
        public decimal Fee { get; protected set; }
    }
}