using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Infrastructure.Events;
using CleanDddHexagonal.Infrastructure.Persistence;
using CleanDddHexagonal.Infrastructure.Repositories;
using CleanDddHexagonal.Infrastructure.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanDddHexagonal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<ICustomerAccountRepository, EfCustomerAccountRepository>();
        services.AddScoped<IUsageRecordRepository, EfUsageRecordRepository>();
        services.AddScoped<IReconciliationIssueRepository, EfReconciliationIssueRepository>();

        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<IDomainEventDispatcher, LoggingDomainEventDispatcher>();

        return services;
    }
}
