using MassTransit;
using Publisher.Models;

namespace Publisher.Services;

public class RabbitMqMessagePublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqMessagePublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync(string content)
    {
        var message = new MessageModel
        {
            Id = Guid.NewGuid(),
            Content = content,
            Timestamp = DateTime.UtcNow
        };

        await _publishEndpoint.Publish(message);
    }
}
