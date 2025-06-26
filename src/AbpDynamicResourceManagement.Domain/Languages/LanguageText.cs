using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace AbpDynamicResourceManagement.Languages;
public class LanguageText : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }

    public string ResourceName { get; set; }

    public string CultureName { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public LanguageText()
    {
    }

    public LanguageText(Guid id, string resourceName, string cultureName, string name, string value = null, Guid? tenantId = null)
    {
        Check.NotNullOrWhiteSpace(resourceName, nameof(resourceName));
        Check.NotNullOrWhiteSpace(cultureName, nameof(cultureName));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Id = id;
        ResourceName = resourceName;
        CultureName = cultureName;
        Name = name;
        Value = value;
        TenantId = tenantId;
    }
}
