using System;
using System.Threading.Tasks;
using Consumer.Models;
using MassTransit;
using StackExchange.Redis;

namespace Consumer.Services;
public interface IMessageConsumer
{
    Task ConsumeAsync();
}