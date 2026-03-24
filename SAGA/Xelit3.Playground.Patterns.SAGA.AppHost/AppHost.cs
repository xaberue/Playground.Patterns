var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql-server")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume(isReadOnly: false).
    WithManagementPlugin();

var sqlDb = sqlServer.AddDatabase("sql-database");

builder.AddProject<Projects.Xelit3_Playground_Patterns_SAGA_Discounts>("discounts-api");

builder.AddProject<Projects.Xelit3_Playground_Patterns_SAGA_Payments>("payments-api");

builder.AddProject<Projects.Xelit3_Playground_Patterns_SAGA_Plans>("plans-api");

builder.AddProject<Projects.Xelit3_Playground_Patterns_SAGA_Orchestrator>("saga-orchestrator")
    .WithReference(sqlDb)
    .WaitFor(sqlDb)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithUrl("/hangfire", "Hangfire Dashboard");

builder.Build().Run();
