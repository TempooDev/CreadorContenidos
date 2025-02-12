var builder = DistributedApplication.CreateBuilder(args);

//Add Azure Storage 

var storage = builder.AddAzureStorage("storage").RunAsEmulator(azurite =>
                     {
                         azurite.WithLifetime(ContainerLifetime.Persistent);
                     }).AddBlobs("images");



var deepseekR1 = builder.AddOllama("ollama").AddModel("deepseek-r1:1.5b");

var apiService = builder.AddProject<Projects.CreadorContenido_ApiService>("apiservice")
            .WithReference(storage)
            .WithReference(deepseekR1)
            .WaitFor(storage);

builder.AddProject<Projects.CreadorContenido_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
