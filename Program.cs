using DotNetEnv;
using EcommerceBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using EcommerceBackend.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var frontendUrl = builder.Configuration["FrontendUrl"] ?? Environment.GetEnvironmentVariable("FRONTEND_URL")!;

Console.WriteLine("Frontend URL: " + frontendUrl);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins(frontendUrl)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

builder.Services.PostConfigure<MongoDbSettings>(
    options =>
    {
        var conn = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        var dbName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME");

        if (!string.IsNullOrEmpty(conn))
        {
            options.ConnectionString = conn;
        }
        if (!string.IsNullOrEmpty(dbName))
        {
            options.DatabaseName = dbName;
        }
    }
    );

// Register MongoDB client and database
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// register the services
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<MenuItemService>();

// add controller
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowNextJs");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
