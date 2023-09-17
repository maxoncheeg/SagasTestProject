using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using SagasTestProject.SagasService.States;

namespace SagasTestProject.SagasService.Model
{
    public sealed class SagaStateMap : SagaClassMap<BuyItemsSagaState>
    {
        protected override void Configure(EntityTypeBuilder<BuyItemsSagaState> entity, ModelBuilder model)
        {
            base.Configure(entity, model);
            entity.Property(x => x.CurrentState).HasMaxLength(255);
        }
    }
}
