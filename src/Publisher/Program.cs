using MassTransit;
using Publisher.Models;
using Publisher.Services;
using RabbitMQPublisher.Settings;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqSettings = new RabbitMqSettings();
builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMqSettings);

builder.Services.AddSingleton(rabbitMqSettings);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqSettings.Host, h =>
        {
            h.Username(rabbitMqSettings.User);
            h.Password(rabbitMqSettings.Password);
        });
    });
});

builder.Services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/publish", async (IMessagePublisher publisher, PublishRequest request) =>
{
    await publisher.PublishAsync(request.Content);
    return Results.Ok(new { Message = "Message Published" });
})
.WithName("PublishMessage")
.WithOpenApi();

app.Run();

