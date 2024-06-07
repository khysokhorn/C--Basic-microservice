using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

// Configure MongoDB settings
var mongoSettings = configuration.GetSection(nameof(MogoDBSetting)).Get<MogoDBSetting>();
builder.Services.AddSingleton(mongoSettings);

// Register MongoDB client and database
builder.Services.AddSingleton(serviceProvider =>
{
    var mongoClient = new MongoClient(mongoSettings.ConnectionString);
    return mongoClient.GetDatabase("Movie-Database");
});

// Add controllers or other services
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
