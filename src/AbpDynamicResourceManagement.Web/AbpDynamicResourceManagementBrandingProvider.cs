using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using AbpDynamicResourceManagement.Localization;

namespace AbpDynamicResourceManagement.Web;

[Dependency(ReplaceServices = true)]
public class AbpDynamicResourceManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AbpDynamicResourceManagementResource> _localizer;

    public AbpDynamicResourceManagementBrandingProvider(IStringLocalizer<AbpDynamicResourceManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
