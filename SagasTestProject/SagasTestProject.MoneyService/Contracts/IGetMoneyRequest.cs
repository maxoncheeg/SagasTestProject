namespace SagasTestProject.MoneyService.Contracts
{
    public interface IGetMoneyRequest
    {
        public Guid OrderId { get; }
        public float? Money { get; }
    }
}
