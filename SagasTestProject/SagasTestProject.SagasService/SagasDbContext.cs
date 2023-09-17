using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagasTestProject.SagasService.Model;

namespace SagasTestProject.SagasService
{
    internal class SagasDbContext : SagaDbContext
    {
        public SagasDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations => new ISagaClassMap[]
        {
            new SagaStateMap()
        };
    }
}
