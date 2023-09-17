using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagasTestProject.SagasService
{
    internal class SagasDbContext : SagaDbContext
    {
        public SagasDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations => new ISagaClassMap[]
        {
            //new SagaStateMap();
        };
    }
}
