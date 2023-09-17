namespace SagasTestProject.ItemsService.Contracts
{
    public interface IGetItemsRequest
    {
        public Guid OrderId { get; }
        public float Money { get; }
    }
}
