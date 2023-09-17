namespace SagasTestProject.MoneyService.Contracts
{
    public interface IGetMoneyResponse
    {
        public Guid OrderId { get; }
        public float? Money { get; }
    }
}
