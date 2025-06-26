using AbpDynamicResourceManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpDynamicResourceManagement.Languages;
public class LanguageTextRepository : EfCoreRepository<AbpDynamicResourceManagementDbContext, LanguageText, Guid>, ILanguageTextRepository
{
    public LanguageTextRepository(IDbContextProvider<AbpDynamicResourceManagementDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<LanguageText> GetdByCultureNameAndNameAsync(string cultureName, string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(l => l.CultureName == cultureName && l.Name == name);
    }

    public async Task<List<LanguageText>> GetListByCultureNameAsync(string cultureName)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(l => l.CultureName == cultureName).ToListAsync();
    }
}
