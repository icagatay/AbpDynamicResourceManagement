using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AbpDynamicResourceManagement.Data;
using Volo.Abp.DependencyInjection;

namespace AbpDynamicResourceManagement.EntityFrameworkCore;

public class EntityFrameworkCoreAbpDynamicResourceManagementDbSchemaMigrator
    : IAbpDynamicResourceManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAbpDynamicResourceManagementDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AbpDynamicResourceManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AbpDynamicResourceManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
