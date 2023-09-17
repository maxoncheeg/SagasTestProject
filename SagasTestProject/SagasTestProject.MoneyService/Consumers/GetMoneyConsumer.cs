using MassTransit;
using SagasTestProject.MoneyService.Contracts;

namespace SagasTestProject.MoneyService.Consumers
{
    public class GetMoneyConsumer : IConsumer<IGetMoneyRequest>
    {
        public async Task Consume(ConsumeContext<IGetMoneyRequest> context)
        {
            using var stream = File.Open(@"C:\Users\maksg\Desktop\money.txt", FileMode.Open);
            using var reader = new StreamReader(stream);

            var money = await reader.ReadToEndAsync();

            if(!float.TryParse(money, out var result))
            {
                result = 0;
            }

            using var stream1 = File.Open(@"C:\Users\maksg\Desktop\moneyInfo.txt", FileMode.Append);
            using var writer = new StreamWriter(stream1);
            await writer.WriteLineAsync(DateTime.Now.ToLongTimeString());

            var respond = new { OrderId = new Guid(), Money = result };
            await context.RespondAsync<IGetMoneyResponse>(respond);
        }
    }
}
