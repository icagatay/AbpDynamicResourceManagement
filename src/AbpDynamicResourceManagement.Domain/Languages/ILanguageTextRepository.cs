using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AbpDynamicResourceManagement.Languages;
public interface ILanguageTextRepository : IRepository<LanguageText, Guid>
{
    Task<LanguageText> GetdByCultureNameAndNameAsync(string cultureName, string name);
    Task<List<LanguageText>> GetListByCultureNameAsync(string cultureName);
}
