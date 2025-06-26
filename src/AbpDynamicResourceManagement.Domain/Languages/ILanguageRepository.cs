using System;
using Volo.Abp.Domain.Repositories;

namespace AbpDynamicResourceManagement.Languages;
public interface ILanguageRepository : IRepository<Language, Guid>
{
}
