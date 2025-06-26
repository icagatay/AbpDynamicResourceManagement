using AbpDynamicResourceManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpDynamicResourceManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpDynamicResourceManagementEntityFrameworkCoreModule),
    typeof(AbpDynamicResourceManagementApplicationContractsModule)
)]
public class AbpDynamicResourceManagementDbMigratorModule : AbpModule
{
}
