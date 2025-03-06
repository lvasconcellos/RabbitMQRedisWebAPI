
CREATE PROJECT

dotnet new sln --name RabbitMQRedisWebAPI

cd src
dotnet new webapi --name Publisher
cd Publisher
dotnet sln ../../RabbitMQRedisWebAPI.sln add Publisher.csproj

cd src
dotnet new webapi --name Consumer
cd Consumer
dotnet sln ../../RabbitMQRedisWebAPI.sln add Consumer.csproj

cd src
dotnet new webapi --name MessageAPI
cd MessageAPI
dotnet sln ../../RabbitMQRedisWebAPI.sln add MessageAPI.csproj

CREATE DOCKER SERVICES

docker-compose up -d

