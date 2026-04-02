using Hangfire;
using Wolverine;
using Wolverine.RabbitMQ;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;
using Xelit3.Playground.Patterns.SAGA.Orchestrator.Scheduler;
using Xelit3.Playground.Patterns.SAGA.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<BillingDbContext>(connectionName: "sqldb");
builder.AddSqlServerClient(connectionName: "sqldb");

builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(new Uri(builder.Configuration.GetConnectionString("rabbitmq")!)).AutoProvision();
    
    opts.PublishMessage<BillingPlansRequestedEvent>().ToRabbitQueue("billingjob-plans-requested-queue");
    opts.PublishMessage<DiscountRequestedForBillingEvent>().ToRabbitQueue("billingjob-discount-requested-queue");
    opts.PublishMessage<AmountCalculationRequestedForBillingEvent>().ToRabbitQueue("billingjob-plan-amount-calculation-requested-queue");
    opts.PublishMessage<PaymentRequestedForBillingEvent>().ToRabbitQueue("billingjob-payment-requested-queue");
    opts.PublishMessage<PlanUpdateRequestedForBillingEvent>().ToRabbitQueue("billingjob-plan-update-requested-queue");
    opts.PublishMessage<PlanCompletedForBillingEvent>().ToLocalQueue("billingjob-plan-completed-queue");

    opts.ListenToRabbitQueue("billingjob-plan-ready-queue");
    opts.ListenToRabbitQueue("billingjob-discount-ready-queue");
    opts.ListenToRabbitQueue("billingjob-payment-calculation-ready-queue");
    opts.ListenToRabbitQueue("billingjob-payment-result-ready-queue");
    opts.ListenToRabbitQueue("billingjob-plan-update-ready-queue");
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