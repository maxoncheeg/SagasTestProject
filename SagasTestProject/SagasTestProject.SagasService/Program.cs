using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using SagasTestProject.SagasService;
using SagasTestProject.SagasService.Model;
using SagasTestProject.SagasService.States;
using IHost = Microsoft.Extensions.Hosting.IHost;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

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
                    //r.LockStatementProvider = new PostgresLockStatementProvider();
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

host.Run();
