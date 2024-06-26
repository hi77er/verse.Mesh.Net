﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using StackExchange.Redis;
using verse.Mesh.Net.Infrastructure.Clients.Base;
using verse.Mesh.Net.Infrastructure.Data.Config;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.Infrastructure;

public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    bool isDevelopmentEnv)
  {
    // Options pattern for strongly typed RedisConfiguration from appSettings.json:
    // (for "Bind()" method Microsoft.Etensions.Configuration.Binder package was installed)
    var redisConfigOptionsName = "RedisOptions";
    var redisConfig = new RedisOptions();
    config.GetSection(redisConfigOptionsName).Bind(redisConfig);

    if (isDevelopmentEnv)
    {
      services.AddMemoryCache();
      services.AddSingleton<IDistributedCacheAdapter, MemoryCacheService>();
    }
    else
    {
      // Register Redis connection
      services.AddSingleton<IConnectionMultiplexer>(sp =>
      {
        // Options pattern is not needed for getting the RedisConfiguration.
        // It is only for DI purpose.
        var options = config.GetSection(redisConfigOptionsName).Get<RedisOptions>();
        var connStr = options?.ConnectionString ?? string.Empty;

        return ConnectionMultiplexer.Connect(connStr);
      });

      services.AddSingleton<IDistributedCacheAdapter, RedisCacheService>();
    }

    //string? connectionString = config.GetConnectionString("SqliteConnection");
    //Guard.Against.Null(connectionString);
    //services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

    //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    //services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    //services.AddScoped<IDeleteContributorService, DeleteContributorService>();

    //services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

    var baseUrl = Environment.GetEnvironmentVariable(AppConstants.INVENTORY_SERVICE_BASE_URL_ENV_VAR_NAME);
    services.AddScoped(sp => ServiceFactory.CreateInventoryService(baseUrl ?? string.Empty));

    //logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
