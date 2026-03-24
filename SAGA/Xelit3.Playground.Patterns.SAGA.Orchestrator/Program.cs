using Hangfire;
using Wolverine;
using Wolverine.RabbitMQ;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerClient(connectionName: "sql-database");

builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(builder.Configuration.GetConnectionString("rabbitmq")!);

    opts.PublishMessage<RenewJobExecutionRequestEvent>().ToRabbitQueue("renew-job-execution-queue");
});

builder.Services.AddOpenApi();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("sql-database")));

builder.Services.AddHangfireServer();

builder.Services.AddHostedService<HangfireRenewJobScheduler>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.Run();