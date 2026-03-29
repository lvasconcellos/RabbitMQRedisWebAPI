
namespace Consumer.Settings;
public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
    public string ExchangeName { get; set; } = string.Empty;
}