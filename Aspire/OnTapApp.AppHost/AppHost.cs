var builder = DistributedApplication.CreateBuilder(args);

var pgsqlUser = builder.AddParameter("postgresql-username", secret: true);
var pgsqlPassword = builder.AddParameter("postgresql-password", secret: true);

var postgres = builder.AddPostgres("postgres", pgsqlUser, pgsqlPassword)
    .WithBindMount("../../Docker/beers_db.backup", "/bkp/beers_db.backup")
    .WithDataVolume("beersdb")        
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgWeb();

var beersDb = postgres.AddDatabase("beersdb","beers_db");

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume("keycloak")
    .WithLifetime(ContainerLifetime.Persistent);

var api = builder.AddProject<Projects.OnTapApp_API>("ontapapp-api")
    .WithReference(beersDb)
    .WaitFor(beersDb)
    .WithReference(keycloak)
    .WaitFor(keycloak);
    //.WithReference(beersDb);

builder.AddProject<Projects.OnTapApp_Web>("ontapapp-web")
    .WithExternalHttpEndpoints()
    .WithReference(keycloak)
    .WithReference(api)    
    .WaitFor(api);



builder.Build().Run();
