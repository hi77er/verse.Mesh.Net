﻿// INFO: EF Core not suitable for NativeAOT Compilated projects 
// because it levarages reflection usage.

//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//
//namespace Clean.Architecture.Infrastructure.Data;
//
//public static class AppDbContextExtensions
//{
//  public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
//  {
//    services.AddDbContext<AppDbContext>(options =>
//         options.UseSqlite(connectionString));
//  }
//}
