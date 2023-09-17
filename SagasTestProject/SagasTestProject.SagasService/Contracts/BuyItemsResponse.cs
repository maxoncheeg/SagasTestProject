namespace SagasTestProject.SagasService.Contracts
{
    public class BuyItemsResponse
    {
        public Guid OrderId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
