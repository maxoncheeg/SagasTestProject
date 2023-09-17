using MassTransit;
using SagasTestProject.ItemsService.Contracts;

namespace SagasTestProject.ItemsService.Consumers
{
    public class GetItemsConsumer : IConsumer<IGetItemsRequest>
    {
        public Task Consume(ConsumeContext<IGetItemsRequest> context)
        {
            return context.RespondAsync<IGetItemsResponse>(new { OrderId = new Guid(), Money = 10f });
        }
    }
}
