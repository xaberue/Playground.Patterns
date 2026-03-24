using Hangfire;
using Wolverine;
using Wolverine.RabbitMQ;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;
using Xelit3.Playground.Patterns.SAGA.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<BillingDbContext>(connectionName: "sqldb");
builder.AddSqlServerClient(connectionName: "sqldb");

builder.Host.UseWolverine(opts =>
{
    //opts.UseRabbitMq(builder.Configuration.GetConnectionString("rabbitmq")!);
    opts.UseRabbitMq(new Uri(builder.Configuration.GetConnectionString("rabbitmq")!)).AutoProvision();

    opts.PublishMessage<BillingJobExecutionRequestEvent>().ToRabbitExchange("renew-job-execution-queue");

    //opts.UseRabbitMqUsingNamedConnection("rabbitmq").AutoProvision();
});

builder.Services.AddOpenApi();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("sqldb")));

builder.Services.AddHangfireServer();

builder.Services.AddHostedService<BillingJobScheduler>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHangfireDashboard();

    await app.AutomateDbMigrations();
}

app.UseHttpsRedirection();

app.Run();