var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("postgres")
    .AddDatabase("DbConn", "knowyourtoolset");

var api = builder.AddProject<Projects.OpenAi_Sample_Api>("knowyourtoolset-backend-api");

var ui = builder.AddProject<Projects.OpenAi_Sample_Bff>("ui-with-bff")
    .WaitFor(db)
    .WithReference(db)
    .WithEnvironment("RemoteApis__api", api.GetEndpoint("https"));

builder.Build().Run();