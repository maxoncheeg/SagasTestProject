//using GreenPipes;
using MassTransit;
using SagasTestProject.MoneyService;
using SagasTestProject.MoneyService.Consumers;
using IHost = Microsoft.Extensions.Hosting.IHost;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddMassTransit(service =>
        {
            service.SetKebabCaseEndpointNameFormatter();

            service.AddDelayedMessageScheduler();

            service.AddConsumer<GetMoneyConsumer>();
            //service.AddConsumer<AddMoneyConsumer>();

            service.UsingRabbitMq((registration, config) =>
            {
                config.UseInMemoryOutbox();
                config.UseMessageRetry(retry =>
                {
                    retry.Interval(3, TimeSpan.FromSeconds(1));
                });
                config.UseDelayedMessageScheduler();

                config.Host("localhost", h =>
                {
                    h.Username("rmuser");
                    h.Password("rmpassword");
                });

                config.ConfigureEndpoints(registration);
            });
        });
    })
    .Build();

host.Run();
