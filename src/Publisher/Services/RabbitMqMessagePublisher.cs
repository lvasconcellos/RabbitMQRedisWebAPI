using MassTransit;
using Publisher.Models;

namespace Publisher.Services
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly IBus _bus;

        public RabbitMqMessagePublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Message content cannot be empty.");

            var message = new MessageModel
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Content = content
            };

            await _bus.Publish<MessageModel>(message);
        }
    }
}