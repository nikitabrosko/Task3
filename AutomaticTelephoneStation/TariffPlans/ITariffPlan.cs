namespace AutomaticTelephoneStation.TariffPlans
{
    public interface ITariffPlan
    {
        public string Name { get; }
        public decimal Fee { get; }
    }
}