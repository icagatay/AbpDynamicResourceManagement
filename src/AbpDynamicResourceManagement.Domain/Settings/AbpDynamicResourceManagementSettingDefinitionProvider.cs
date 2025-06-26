using Volo.Abp.Settings;

namespace AbpDynamicResourceManagement.Settings;

public class AbpDynamicResourceManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AbpDynamicResourceManagementSettings.MySetting1));
    }
}
