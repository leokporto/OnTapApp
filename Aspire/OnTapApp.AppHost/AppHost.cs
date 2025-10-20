using Aspire.Hosting;
using System.Runtime.InteropServices;

var builder = DistributedApplication.CreateBuilder(args);



//var postgres = builder.AddPostgres("postgres");
//var beersDb = postgres.AddDatabase("beersdb","beers_db");


var api = builder.AddProject<Projects.OnTapApp_API>("ontapapp-api");
    //.WithReference(beersDb);

builder.AddProject<Projects.OnTapApp_Web>("ontapapp-web")
    .WithExternalHttpEndpoints()
    .WithReference(api)
    .WaitFor(api);



builder.Build().Run();
