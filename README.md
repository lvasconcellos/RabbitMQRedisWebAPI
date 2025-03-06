# RabbitMQ & Redis Messaging System

## Overview
This project demonstrates how to send and receive messages using **RabbitMQ** with **C#**, and cache received messages using **Redis**. The system consists of three main components:

1. **Publisher**: Sends messages to RabbitMQ.
2. **Consumer**: Listens for messages from RabbitMQ and stores them in Redis.
3. **Message API**: Provides an endpoint to retrieve cached messages from Redis.

---

## Tech Stack
- **C#** (.NET 9)
- **RabbitMQ** (Message Broker)
- **Redis** (Caching Layer)
- **MassTransit** (for RabbitMQ Integration)
- **StackExchange.Redis** (for Redis Communication)
- **ASP.NET Web API** (for Exposing Cached Messages)
- **Docker & Docker Compose** (for Containerized RabbitMQ & Redis)

---

## Project Structure
```
/RabbitMQRedisWebAPI
 ├── /Publisher           # Publishes messages to RabbitMQ
 │   ├── Program.cs       # Sends messages
 │   ├── MessageService.cs
 │   └── appsettings.json
 ├── /Consumer            # Consumes messages and caches them in Redis
 │   ├── Program.cs       # Listens for RabbitMQ messages
 │   ├── MessageHandler.cs
 │   ├── RedisService.cs
 │   └── appsettings.json
 ├── /MessageAPI          # Web API for fetching messages from Redis
 │   ├── Program.cs       # ASP.NET Web API
 │   ├── Controllers/
 │   ├── Services/
 │   ├── appsettings.json
 ├── docker-compose.yml   # RabbitMQ & Redis setup
 ├── .env                 # Environment variables (RabbitMQ credentials)
 ├── README.md            # Project documentation
```

---

## Setup & Running
### Prerequisites
- Install **Docker** and **Docker Compose**
- Install **.NET SDK** 9

### 1. Start RabbitMQ & Redis
Run the following command to start RabbitMQ and Redis:
```sh
docker-compose up -d
```
This will start **RabbitMQ** on port `5672` and **Redis** on port `6379`.

### 2. Run the Consumer
```sh
dotnet run --project Consumer
```
This will listen for incoming messages and store them in Redis.

### 3. Run the Publisher
```sh
dotnet run --project Publisher
```
This will send messages to RabbitMQ.

### 4. Run the Web API
```sh
dotnet run --project MessageAPI
```
The API will be available at:
```
http://localhost:5000/api/messages
```
You can retrieve stored messages from Redis using:
```sh
curl http://localhost:5000/api/messages
```

---

## Environment Variables
This project uses a **`.env`** file for storing RabbitMQ credentials. Example:
```
RABBITMQ_USER=mysecureuser
RABBITMQ_PASS=randomsecurepassword
```
Modify the `docker-compose.yml` to load it:
```yaml
env_file:
  - .env
```

---

## License
This project is licensed under the MIT License.

---

## Contact
For questions or improvements, feel free to contribute!