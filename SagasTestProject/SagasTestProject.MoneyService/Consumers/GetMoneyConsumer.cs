using MassTransit;
using SagasTestProject.MoneyService.Contracts;

namespace SagasTestProject.MoneyService.Consumers
{
    public class GetMoneyConsumer : IConsumer<IGetMoneyRequest>
    {
        public Task Consume(ConsumeContext<IGetMoneyRequest> context)
        {
            return context.RespondAsync<IGetMoneyResponse>(new { OrderId = new Guid(), Money = 10f });
        }
    }
}
