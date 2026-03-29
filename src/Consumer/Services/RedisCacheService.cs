using Consumer.Models;
using StackExchange.Redis;

namespace Consumer.Services;


public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _redisDb;

    public RedisCacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        _redisDb = redis.GetDatabase();
    }

    public async Task CacheMessageAsync(MessageModel message)
    {
        string key = $"message:{message.Id}";
        await _redisDb.StringSetAsync(key, message.Content, TimeSpan.FromHours(1));
    }
}