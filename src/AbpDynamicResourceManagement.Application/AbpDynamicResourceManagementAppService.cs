using AbpDynamicResourceManagement.Localization;
using Volo.Abp.Application.Services;

namespace AbpDynamicResourceManagement;

/* Inherit your application services from this class.
 */
public abstract class AbpDynamicResourceManagementAppService : ApplicationService
{
    protected AbpDynamicResourceManagementAppService()
    {
        LocalizationResource = typeof(AbpDynamicResourceManagementResource);
    }
}
