using Consumer.Models;

namespace Consumer.Services;


public interface IRedisCacheService
{
    Task CacheMessageAsync(MessageModel message);
}