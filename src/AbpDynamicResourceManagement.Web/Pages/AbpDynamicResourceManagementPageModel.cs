using AbpDynamicResourceManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AbpDynamicResourceManagement.Web.Pages;

public abstract class AbpDynamicResourceManagementPageModel : AbpPageModel
{
    protected AbpDynamicResourceManagementPageModel()
    {
        LocalizationResourceType = typeof(AbpDynamicResourceManagementResource);
    }
}
