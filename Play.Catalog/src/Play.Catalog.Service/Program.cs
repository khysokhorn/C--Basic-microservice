using Model;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Repository;

ServiceSettings serviceSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
// BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
var mongoSetting = builder.Configuration.GetSection(nameof(MogoDBSetting)).Get<MogoDBSetting>();
// builder.Services.AddSingleton(serviceSettings);
// builder.Services.AddSingleton(mongoSetting);
builder.Services.AddSingleton(serviceProvier =>
{
    var mongoClient = new MongoClient(mongoSetting.ConnectionString);
    return mongoClient.GetDatabase("Movie-Database");
});
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IItemRepository, ItemRepository>();
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
