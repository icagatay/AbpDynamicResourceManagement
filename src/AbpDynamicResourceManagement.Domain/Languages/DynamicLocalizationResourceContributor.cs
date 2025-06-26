using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Threading;

namespace AbpDynamicResourceManagement.Languages;
public class DynamicLocalizationResourceContributor : ILocalizationResourceContributor
{
    private ILanguageTextRepository _languageTextRepository = default!;

    public bool IsDynamic { get; }

    public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        List<LanguageText> messages = AsyncHelper.RunSync((() => _languageTextRepository.GetListByCultureNameAsync(cultureName)));
        foreach (LanguageText message in messages)
        {
            dictionary[message.Name] = new LocalizedString(message.Name, message.Value);
        }
    }

    public async Task FillAsync(string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        var messages = await _languageTextRepository.GetListByCultureNameAsync(cultureName);
        foreach (var message in messages)
        {
            dictionary[message.Name] = new LocalizedString(message.Name, message.Value);
        }
    }

    public LocalizedString? GetOrNull(string cultureName, string name)
    {
        LanguageText message = AsyncHelper.RunSync((() => _languageTextRepository.GetdByCultureNameAndNameAsync(cultureName, name)));
        if (message == default)
        {
            return null;
        }
        return new LocalizedString(name, message.Value);
    }

    public async Task<IEnumerable<string>> GetSupportedCulturesAsync()
    {
        return _languageTextRepository.GetListAsync().Result.Select(x => x.CultureName).Distinct();
    }

    public void Initialize(LocalizationResourceInitializationContext context)
    {
        _languageTextRepository = context.ServiceProvider.GetRequiredService<ILanguageTextRepository>();
    }
}
