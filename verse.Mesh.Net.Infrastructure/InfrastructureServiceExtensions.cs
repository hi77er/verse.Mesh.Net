using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.Infrastructure;

public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    var redisConfigOptionsName = "RedisConfiguration";

    // Options pattern for strongly typed RedisConfiguration from appSettings.json:
    // (for "Bind()" method Microsoft.Etensions.Configuration.Binder package was installed)
    var redisConfig = new RedisConfiguration();
    config.GetSection(redisConfigOptionsName).Bind(redisConfig);

    // Register Redis connection
    services.AddSingleton<IConnectionMultiplexer>(sp =>
    {
      // Options pattern is not needed for getting the RedisConfiguration.
      // It is only for DI purpose.
      var options = config.GetSection(redisConfigOptionsName).Get<RedisConfiguration>();
      var connStr = options?.ConnectionString ?? string.Empty;

      return ConnectionMultiplexer.Connect(connStr);
    });




    //string? connectionString = config.GetConnectionString("SqliteConnection");
    //Guard.Against.Null(connectionString);
    //services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

    //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    //services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    services.AddScoped<IRedisService, RedisService>();
    //services.AddScoped<IDeleteContributorService, DeleteContributorService>();

    //services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
