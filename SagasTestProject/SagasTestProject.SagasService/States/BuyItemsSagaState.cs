﻿using Automatonymous;

namespace SagasTestProject.SagasService.States
{
    internal class BuyItemsSagaState : MassTransit.SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }
        public Guid? RequestId { get; set; }
        public Uri? ResponseAddress { get; set; }

    }
}
