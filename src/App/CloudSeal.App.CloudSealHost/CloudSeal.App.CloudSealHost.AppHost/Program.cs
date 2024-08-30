var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", true);

var password = builder.AddParameter("password", true);

var redis = builder
    //.AddRedis("RedisInstance")
    //.PublishAsConnectionString();
    .AddRedis("RedisInstance", 6379)
    .WithImage("redis", "7.4")
    .WithRedisCommander(config =>
    {
        config.WithHostPort(6380);
    })
    .WithDataVolume("redis-data-volume")
    .WithPersistence();

var postgresql = builder
    //.AddPostgres("PostgresInstance")
    //.PublishAsConnectionString();
    .AddPostgres("PostgresInstance", username, password, 5432)
    .WithImage("postgres", "16.4")
    .WithPgAdmin(config =>
    {
        config.WithHostPort(5440)
            .WithVolume("pgadmin-data-volume", "/var/lib/pgadmin");
    })
    .WithDataVolume("postgres-data-volume")
    .AddDatabase("CloudSeal");

var mongodb = builder
    .AddMongoDB("MongoInstance", 27017)
    .WithImage("mongo", "7.0")
    .WithMongoExpress(config =>
    {
        config.WithHostPort(27020);
    })
    .WithDataVolume("mongo-data-volume")
    .WithVolume("mongo-configdb-volume", "/data/configdb")
    .AddDatabase("document");

var rabbitmq = builder
    .AddRabbitMQ("RabbitMqInstance", username, password, 5672)
    //.WithImage("rabbitmq", "3.13-management")
    .WithManagementPlugin(15672)
    .WithDataVolume("rabbit-data-volume");

//var nats = builder
//    .AddNats("NatsInstance", 4222)
//    .WithImage("nats", "2.10")
//    .WithJetStream()
//    .WithDataVolume("nats-data-volume");

//var qdrant = builder
//    .AddQdrant("QdrantInstance", password, 6334, 6333)
//    .WithDataVolume("qdrant-data-volume");

var orleans = builder.AddOrleans("voting-cluster")
    .WithClustering(redis)
    .WithGrainStorage("sample", redis);

builder.Build().Run();
