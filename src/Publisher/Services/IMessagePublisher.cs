namespace Publisher.Services;

public interface IMessagePublisher
{
    Task PublishAsync(string content);
}
