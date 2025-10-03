using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("webhooks");

builder.AddProject<Projects.WebHooks_API>("WebHooks-API")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();