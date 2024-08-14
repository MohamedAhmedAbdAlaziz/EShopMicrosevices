﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.AddInterceptors(new AuditableEntityInterceptor());
                option.UseSqlServer(connection);
            });


            return services;
        }
    }
}
