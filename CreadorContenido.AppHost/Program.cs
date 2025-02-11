var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.CreadorContenido_ApiService>("apiservice");

builder.AddProject<Projects.CreadorContenido_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
