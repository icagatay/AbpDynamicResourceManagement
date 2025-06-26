using AbpDynamicResourceManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpDynamicResourceManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AbpDynamicResourceManagementController : AbpControllerBase
{
    protected AbpDynamicResourceManagementController()
    {
        LocalizationResource = typeof(AbpDynamicResourceManagementResource);
    }
}
