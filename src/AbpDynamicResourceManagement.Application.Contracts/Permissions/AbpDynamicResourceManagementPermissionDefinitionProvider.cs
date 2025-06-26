using AbpDynamicResourceManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpDynamicResourceManagement.Permissions;

public class AbpDynamicResourceManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpDynamicResourceManagementPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(AbpDynamicResourceManagementPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(AbpDynamicResourceManagementPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(AbpDynamicResourceManagementPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(AbpDynamicResourceManagementPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AbpDynamicResourceManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpDynamicResourceManagementResource>(name);
    }
}
