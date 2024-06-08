using System.Reflection;
using Entities;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Commom.Repository
{
    public static class Extentions
    {

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.UsingRabbitMq((context, confi) =>
                {

                    var configuration = context.GetService<IConfiguration>();
                    var rabbitMQSetting = configuration!.GetSection(nameof(RabbitMQSetting)).Get<RabbitMQSetting>();
                    var serviceSetting = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    confi.Host(rabbitMQSetting!.Host);
                    confi.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSetting!.ServiceName, false));
                });
            });
            return services;
        }
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
            services.AddSingleton(serviceProvier =>
            {
                var config = serviceProvier.GetService<IConfiguration>();
                var serviceSettings = config?.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoSetting = config?.GetSection(nameof(MogoDBSetting)).Get<MogoDBSetting>();
                var mongoClientDB = new MongoClient(mongoSetting?.ConnectionString)
                .GetDatabase(mongoSetting?.DataBaseName);
                return mongoClientDB;
            });
            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {
                return new MongoRepository<T>(database: serviceProvider.GetService<IMongoDatabase>(), collectionName);
            });
            return services;
        }

    }
}