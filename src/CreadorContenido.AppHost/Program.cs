var builder = DistributedApplication.CreateBuilder(args);

//Add Azure Storage 

var storage = builder.AddAzureStorage("storage").RunAsEmulator(azurite =>
                     {
                         azurite.WithLifetime(ContainerLifetime.Persistent);
                     }).AddBlobs("images");


var apiService = builder.AddProject<Projects.CreadorContenido_ApiService>("apiservice")
            .WithReference(storage)
            .WaitFor(storage);

builder.AddProject<Projects.CreadorContenido_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
