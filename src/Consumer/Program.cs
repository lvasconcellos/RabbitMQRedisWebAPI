using MassTransit;
using Consumer.Settings;
using Consumer.Models;
using Consumer.Services;
using MassTransit.Transports.Fabric;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);


// Load RabbitMQ settings
var rabbitMqSettings = new RabbitMqSettings();
builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMqSettings);
builder.Services.AddSingleton(rabbitMqSettings);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(rabbitMqSettings.Host), h =>
      {
          h.Username(rabbitMqSettings.User);
          h.Password(rabbitMqSettings.Password);
      });

        cfg.ReceiveEndpoint(rabbitMqSettings.QueueName, ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<MessageConsumer>(context);
            ep.Bind(rabbitMqSettings.ExchangeName, x =>
            {
                x.ExchangeType = ExchangeType.FanOut.ToString().ToLower();
            });
        });
    });
});

var app = builder.Build();
app.Run();
