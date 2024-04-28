using System.Reflection;
using System.Text.Json.Serialization;
using Ardalis.ListStartupServices;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Serilog;
using Serilog.Extensions.Logging;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Core.Shared.Behavior;
using verse.Mesh.Net.Infrastructure;
using verse.Mesh.Net.UseCases;
using verse.Mesh.Net.UseCases.Carts;


var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting Inventory Service host");

var builder = WebApplication.CreateSlimBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger).CreateLogger<Program>();

builder.Services
  .ConfigureHttpJsonOptions(options =>
  {
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
  });

builder.Services
  .AddFastEndpoints()
  .SwaggerDocument(o =>
  {
    o.ShortSchemaNames = true;
  });

ConfigureMediatR();

var isDevelopmentEnv = builder.Environment.IsDevelopment();
builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger, isDevelopmentEnv);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices

  SeedDatabase(app);
}
else
{
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}

app.UseFastEndpoints()
    .UseSwaggerGen(); // Includes AddFileServer and static files middleware

app.UseHttpsRedirection();

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
  //using var scope = app.Services.CreateScope();
  //var services = scope.ServiceProvider;

  //try
  //{
  //  var context = services.GetRequiredService<AppDbContext>();
  //  //          context.Database.Migrate();
  //  context.Database.EnsureCreated();
  //  SeedData.Initialize(services);
  //}
  //catch (Exception ex)
  //{
  //  var logger = services.GetRequiredService<ILogger<Program>>();
  //  logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  //}
}

void ConfigureMediatR()
{
  var mediatRAssemblies = new[]
  {
    Assembly.GetAssembly(typeof(Cart)), // Core
    Assembly.GetAssembly(typeof(CartDTO)) // UseCases
  };
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
  builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

[JsonSerializable(typeof(ErrorResponse))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }

// Make the implicit Program.cs class public, so integration tests can reference the
// correct assembly for host building
namespace verse.Mesh.Net.CheckoutService
{
  public partial class Program { }
}
