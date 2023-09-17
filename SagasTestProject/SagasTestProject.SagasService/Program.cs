using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagasTestProject.SagasService;
using SagasTestProject.SagasService.Model;
using SagasTestProject.SagasService.States;
using IHost = Microsoft.Extensions.Hosting.IHost;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        //services.AddHostedService<Worker>();
        services.AddDbContext<SagasDbContext>(option => option.UseSqlite(host.Configuration.GetConnectionString("default")));

        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddDelayedMessageScheduler();
            cfg.AddSagaStateMachine<BuyItemsSaga, BuyItemsSagaState>()
                .EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                    r.ExistingDbContext<SagasDbContext>();
                    r.LockStatementProvider = new SqliteLockStatementProvider();
                });
            cfg.UsingRabbitMq((brc, rbfc) =>
            {
                rbfc.UseInMemoryOutbox();
                rbfc.UseMessageRetry(r => 
                { 
                    r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1)); 
                });
                rbfc.UseDelayedMessageScheduler();
                rbfc.Host("localhost", h =>
                {
                    h.Username("rmuser");
                    h.Password("rmpassword");
                });
                rbfc.ConfigureEndpoints(brc);
            });
        });
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SagasDbContext>();
    db.Database.Migrate();
}

host.Run();
