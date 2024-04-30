using System.Reflection;
using System.Text.Json.Serialization;
using Ardalis.ListStartupServices;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;
using verse.Mesh.Net.CartService.Commands;
using verse.Mesh.Net.CartService.Health;
using verse.Mesh.Net.CartService.Models;
using verse.Mesh.Net.CartService.Queries;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Core.Shared.Behavior;
using verse.Mesh.Net.Infrastructure;
using verse.Mesh.Net.Infrastructure.Data;
using verse.Mesh.Net.Infrastructure.Data.MemCache;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Get;
using verse.Mesh.Net.UseCases.Products;


var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
  loggingBuilder.AddConsole(); // Add console logging provider (example)
});

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger).CreateLogger<Program>();

builder.Services
  .ConfigureHttpJsonOptions(options =>
  {
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
  });


ConfigureMediatR();

var isDevelopmentEnv = builder.Environment.IsDevelopment();
builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger, isDevelopmentEnv);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  //app.UseDeveloperExceptionPage();
  //app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices

  SeedDatabase(app);
}
else
{
  //app.UseDefaultExceptionHandler(); // from FastEndpoints
  //app.UseHsts();
}

app.UseHttpsRedirection();

app.MapHealthEndpoint();
app.MapGetByUserEndpoint();
app.MapCreateCartEndpoint();

//var sampleTodos = new Todo[] {
//    new(1, "Walk the dog"),
//    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
//    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
//    new(4, "Clean the bathroom"),
//    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
//};

//var todosApi = app.MapGroup("/todos");
//todosApi.MapGet("/", () => sampleTodos);
//todosApi.MapGet("/{id}", (int id) =>
//    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
//        ? Results.Ok(todo)
//        : Results.NotFound());

app.Run();

static void SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var service = services.GetRequiredService<IDistributedCacheAdapter>();
    var dataSeeder = new RedisDataSeeder(service);
    dataSeeder.Seed();
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

void ConfigureMediatR()
{
  var mediatRAssemblies = new[]
  {
    Assembly.GetAssembly(typeof(Cart)), // Core
    Assembly.GetAssembly(typeof(CartDTO)) // UseCases
  };
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
  //builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

[JsonSerializable(typeof(GetHealthRequest))]
[JsonSerializable(typeof(GetCartByUserRequest))]
[JsonSerializable(typeof(GetCartByUserQuery))]
[JsonSerializable(typeof(CartDTO))]
[JsonSerializable(typeof(CartItemDTO))]
[JsonSerializable(typeof(ProductDTO))]
[JsonSerializable(typeof(CreateCartRequest))]
[JsonSerializable(typeof(CreateCartResponse))]
[JsonSerializable(typeof(CreateCartRecord))]
[JsonSerializable(typeof(CartRecord))]
[JsonSerializable(typeof(CartItemRecord))]
[JsonSerializable(typeof(List<CartItemRecord>))]
[JsonSerializable(typeof(IEnumerable<CartItemRecord>))]
[JsonSerializable(typeof(ProductRecord))]
public partial class AppJsonSerializerContext : JsonSerializerContext { }

// Make the implicit Program.cs class public, so integration tests can reference the
// correct assembly for host building
namespace verse.Mesh.Net.CartService
{
  public partial class Program { }
}
