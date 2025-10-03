using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("webhooks");

var queue = builder.AddRabbitMQ("rabbitmq")
    .WithDataVolume()
    .WithManagementPlugin();

builder.AddProject<WebHooks_API>("WebHooks-API")
    .WithReference(database)
    .WithReference(queue)
    .WaitFor(database)
    .WaitFor(queue);

builder.Build().Run();