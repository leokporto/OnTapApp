using Aspire.Hosting;
using System.Runtime.InteropServices;

var builder = DistributedApplication.CreateBuilder(args);

var pgsqlUser = builder.AddParameter("username", secret: true);
var pgsqlPassword = builder.AddParameter("password", secret: true);

var beersDbConnection = builder.AddConnectionString("BeersDbConnection");

var postgres = builder.AddPostgres("postgres", pgsqlUser, pgsqlPassword)
    .WithDataVolume()
    .WithPgWeb();

var beersDb = postgres.AddDatabase("beersdb","beers_db");

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume();

var api = builder.AddProject<Projects.OnTapApp_API>("ontapapp-api")
    .WithReference(keycloak)
    .WaitFor(keycloak);
    //.WithReference(beersDb);

builder.AddProject<Projects.OnTapApp_Web>("ontapapp-web")
    .WithExternalHttpEndpoints()
    .WithReference(keycloak)
    .WithReference(api)    
    .WaitFor(api);



builder.Build().Run();
