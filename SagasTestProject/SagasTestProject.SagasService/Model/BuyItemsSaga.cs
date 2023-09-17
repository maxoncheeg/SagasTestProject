
using MassTransit;
using SagasTestProject.ItemsService.Contracts;
using SagasTestProject.MoneyService.Contracts;
using SagasTestProject.SagasService.Contracts;
using SagasTestProject.SagasService.States;

namespace SagasTestProject.SagasService.Model
{
    internal sealed class BuyItemsSaga : MassTransitStateMachine<BuyItemsSagaState>
    {
        private readonly ILogger<BuyItemsSaga> _logger;

        public Event<BuyItemsRequest> BuyItems { get; set; }
        public State Failed { get; set; }

        public Request<BuyItemsSagaState, IGetMoneyRequest, IGetMoneyResponse> GetMoney { get; set; }
        public Request<BuyItemsSagaState, IGetItemsRequest, IGetItemsResponse> GetItems { get; set; }

        public BuyItemsSaga(ILogger<BuyItemsSaga> logger)
        {
            _logger = logger;
            InstanceState(x => x.CurrentState);

            Event(() => BuyItems, x => x.CorrelateById(y => y.Message.OrderId));

            Request(() => GetMoney);
            Request(() => GetItems);

            Initially(When(BuyItems).Then(x =>
            {
                if (!x.TryGetPayload(out SagaConsumeContext<BuyItemsSagaState, BuyItemsRequest> payload))
                    throw new Exception("Unable to retrieve required payload for callback data.");

                
                x.Saga.RequestId = payload.RequestId;
                x.Saga.ResponseAddress = payload.ResponseAddress;

            }).Request(GetMoney, x => x.Init<IGetMoneyRequest>(new { OrderId = x.Data.OrderId }))
           .TransitionTo(GetMoney.Pending));
        }

        private static async Task RespondFromSaga<T>(BehaviorContext<BuyItemsSagaState, T> context, string error) where T : class
        {
            var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
            await endpoint.Send(new BuyItemsResponse
            {
                OrderId = context.Saga.CorrelationId,
                ErrorMessage = error
            }, r => r.RequestId = context.Saga.RequestId);
        }
    }
}
