using Consumer.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Consumer.Services
{
    public class MessageConsumer : IConsumer<MessageModel>
    {
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MessageModel> context)
        {
            _logger.LogInformation($"📥 Received message: {context.Message.Content}, ID: {context.Message.Id}, Timestamp: {context.Message.Timestamp}");
            await Task.CompletedTask;
        }
    }
}
