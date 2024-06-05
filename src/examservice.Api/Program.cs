using examservice.Core;
using examservice.Core.Middlewares;
using examservice.Infrastructure;
using examservice.Infrastructure.Data;
using examservice.Service;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Declare Connection String
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
builder.Services.AddStackExchangeRedisCache(action =>
{
    //the redis connection
    action.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});
#endregion

#region DependancyInjections
builder.Services
    .AddInfrastructureDependencies()
    .AddServiceDependencies(builder)
    .AddCoreDependencies();
builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });
#endregion

#region AllowCORS
var CORS = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});

#endregion

#region Serilog
Serilog.Debugging.SelfLog.Enable(Console.Error);

//serilog
Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddSerilog();
#endregion



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase<ApplicationDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors(CORS);

app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard();

app.Run();
