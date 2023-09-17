using MassTransit;
using SagasTestProject.ItemsService.Contracts;

namespace SagasTestProject.ItemsService.Consumers
{
    public class GetItemsConsumer : IConsumer<IGetItemsRequest>
    {
        public async Task Consume(ConsumeContext<IGetItemsRequest> context)
        {
            using var stream = File.Open(@"C:\Users\maksg\Desktop\getItems.txt", FileMode.Append);
            using var writer = new StreamWriter(stream);

            await writer.WriteLineAsync(DateTime.Now.ToLongTimeString() + " " + context.Message.Money.ToString());

            await context.RespondAsync<IGetItemsResponse>(new { OrderId = new Guid(), context.Message.Money });
        }
    }
}
