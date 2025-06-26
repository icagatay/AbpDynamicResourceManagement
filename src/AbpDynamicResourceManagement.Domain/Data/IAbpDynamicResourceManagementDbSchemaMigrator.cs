using System.Threading.Tasks;

namespace AbpDynamicResourceManagement.Data;

public interface IAbpDynamicResourceManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
