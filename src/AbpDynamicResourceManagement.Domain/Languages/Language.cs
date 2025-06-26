using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpDynamicResourceManagement.Languages;
public class Language : FullAuditedAggregateRoot<Guid>, ILanguageInfo, IMultiTenant
{
    public Guid? TenantId { get; private set; }
    public string CultureName { get; protected set; }
    public string UiCultureName { get; protected set; }
    public string DisplayName { get; protected set; }
    public string? FlagIcon { get; set; }
    public virtual bool IsEnabled { get; set; }

    protected Language()
    {
    }

    internal Language(
      Guid id,
      string cultureName,
      string uiCultureName = null,
      string displayName = null,
      string flagIcon = null,
      bool isEnabled = true,
      Guid? tenantId = null) : base(id)
    {
        CultureName = cultureName;
        UiCultureName = uiCultureName;
        SetDisplayName(displayName);
        FlagIcon = flagIcon;
        IsEnabled = isEnabled;
        TenantId = tenantId;
    }

    public virtual void ChangeCulture(string cultureName, string uiCultureName = null, string displayName = null)
    {
        if (CultureName == cultureName && UiCultureName == uiCultureName && DisplayName == displayName)
            return;
        ChangeCultureInternal(cultureName, uiCultureName, displayName);
    }

    protected virtual void ChangeCultureInternal(
      string cultureName,
      string uiCultureName,
      string displayName)
    {
        CultureName = cultureName;
        UiCultureName = uiCultureName;
        DisplayName = displayName;

    }

    public virtual void SetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }
}
