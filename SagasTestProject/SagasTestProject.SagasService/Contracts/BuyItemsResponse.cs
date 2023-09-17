namespace SagasTestProject.SagasService.Contracts
{
    internal class BuyItemsResponse
    {
        public Guid OrderId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
