using Wolverine;
using Wolverine.RabbitMQ;
using Xelit3.Playground.Patterns.SAGA.Contracts;
using Xelit3.Playground.Patterns.SAGA.Plans.Infrastructure;
using Xelit3.Playground.Patterns.SAGA.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseWolverine(opts =>
{
    opts.UseRabbitMq(new Uri(builder.Configuration.GetConnectionString("rabbitmq")!)).AutoProvision();

    opts.ListenToRabbitQueue("billingjob-execution-requested-queue");
    opts.PublishMessage<PlanReadyForBillingEvent>().ToRabbitQueue("billingjob-plan-ready-queue");
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<PlanRepository>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
