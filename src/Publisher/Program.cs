using MassTransit;
using Publisher.Services;
using RabbitMQPublisher.Settings;
using Publisher.Models;
using MassTransit.Transports.Fabric;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqSettings = new RabbitMqSettings();
builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMqSettings);
builder.Services.AddSingleton(rabbitMqSettings);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(rabbitMqSettings.Host), h =>
          {
              h.Username(rabbitMqSettings.User);
              h.Password(rabbitMqSettings.Password);
          });

        cfg.Publish<MessageModel>(p =>
        {
            p.ExchangeType = ExchangeType.FanOut.ToString().ToLower();
        });

        cfg.Send<MessageModel>(s =>
        {
            s.UseRoutingKeyFormatter(context => rabbitMqSettings.QueueName);
        });
    });
});

// Register the publisher as Singleton
builder.Services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define API Endpoint for Publishing Messages
app.MapPost("/publish", async (IMessagePublisher publisher, PublishRequest request) =>
{
    await publisher.PublishAsync(request.Content);
    return Results.Ok(new { Message = "Message Published" });
})
.WithName("PublishMessage")
.WithOpenApi();

app.Run();
