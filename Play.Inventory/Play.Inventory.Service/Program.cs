using Commom.Repository;
using Inventor;
using ModelInventory;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(1));

Random jitterer = new Random();

builder.Services.AddHttpClient<CatelogClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7291");
})
.AddTransientHttpErrorPolicy(e => e.Or<TimeoutRejectedException>().WaitAndRetryAsync(
    5, retimeTep => TimeSpan.FromSeconds(Math.Pow(2, retimeTep)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)),
    onRetry: (Outcome, timeSpan, retimeTep) =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        serviceProvider.GetService<ILogger<CatelogClient>>()
        .LogWarning($"Delay for {timeSpan} seconds, then retrun at {retimeTep}");
    }
))
.AddTransientHttpErrorPolicy(e => e.Or<TimeoutRejectedException>().CircuitBreakerAsync(
    3, TimeSpan.FromSeconds(15), onBreak: (outcom, timespan) =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        serviceProvider.GetService<ILogger<CatelogClient>>()
        .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds....");
    }, onReset: () =>
    {
        var serviceProvider = builder?.Services?.BuildServiceProvider();
        serviceProvider?.GetService<ILogger<CatelogClient>>()
        .LogWarning($"Closing the circuit.");
    }
))
.AddPolicyHandler(timeoutPolicy);

builder.Services.AddMongo()
.AddMongoRepository<InventoryItems>("Users");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
