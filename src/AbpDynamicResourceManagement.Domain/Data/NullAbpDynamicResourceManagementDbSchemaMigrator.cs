using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpDynamicResourceManagement.Data;

/* This is used if database provider does't define
 * IAbpDynamicResourceManagementDbSchemaMigrator implementation.
 */
public class NullAbpDynamicResourceManagementDbSchemaMigrator : IAbpDynamicResourceManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
