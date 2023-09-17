namespace SagasTestProject.ItemsService.Contracts
{
    public interface IGetItemsResponse
    {
        public Guid OrderId { get; }
        public float? Money { get; }
    }
}
