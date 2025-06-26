# ABP Framework ile Dinamik Lokalizasyon: Veritabanı Tabanlı Esnek Dil Yönetimi

## 🎯 Giriş

ABP Framework, güçlü bir yerelleştirme (localization) altyapısı ile gelir. Ancak varsayılan olarak çeviri metinleri `.resx` dosyalarında sabitlenmiştir. Bu yapı, çevrimiçi yönetim paneli üzerinden dil içeriğini değiştirme, çok kiracılı uygulamalarda tenant bazlı çeviri sağlama gibi esneklikler sunmaz.

Bu yazıda, **ABP Framework’te dinamik olarak veritabanı destekli lokalizasyon** işlemini nasıl gerçekleştirebileceğimizi adım adım ele alacağız. Örnek olarak oluşturulmuş `AbpDynamicResourceManagement` projesinden kod parçalarıyla ilerleyeceğiz.

---

## 🧱 Genel Mimari

Dinamik lokalizasyon sistemi şu temel bileşenlerden oluşur:

- `Language`: Tanımlı dillerin tutulduğu entity
- `LanguageText`: Her dile ait çevirilerin tutulduğu entity
- `ILanguageTextRepository`: Veri erişim işlemleri için repository arayüzü
- `DynamicLocalizationResourceContributor`: ABP'nin localization pipeline’ına entegre edilen özel sağlayıcı
- `AbpLocalizationOptions.GlobalContributors`: Yeni provider’ı tanıtan yapılandırma

---

## 🗣️ Dil Tanımı: `Language` Entity'si

```csharp
public class Language : FullAuditedAggregateRoot<Guid>, ILanguageInfo, IMultiTenant
{
    public Guid? TenantId { get; private set; }
    public string CultureName { get; protected set; }
    public string UiCultureName { get; protected set; }
    public string DisplayName { get; protected set; }
    public string? FlagIcon { get; set; }
    public bool IsEnabled { get; set; }
    
    // ...
}
```
Bu sınıf, sistemdeki dillerin tanımlandığı temel varlıktır. Çoklu tenant desteği için IMultiTenant arayüzü uygulanmıştır.

## 📝 Çeviri Metinleri: LanguageText Entity'si

```csharp
public class LanguageText : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public string ResourceName { get; set; }
    public string CultureName { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    // ...
}
```

Her bir çeviri metni, ilgili kültür (CultureName) ve anahtar (Name) ile ilişkilendirilmiştir. Örneğin:

| CultureName | Name         | Value   |
| ----------- | ------------ | ------- |
| tr          | HelloMessage | Merhaba |
| en          | HelloMessage | Hello   |


## 📦 Repository Katmanı
Arayüz
```csharp
public interface ILanguageTextRepository : IRepository<LanguageText, Guid>
{
    Task<LanguageText> GetdByCultureNameAndNameAsync(string cultureName, string name);
    Task<List<LanguageText>> GetListByCultureNameAsync(string cultureName);
}
```

Uygulama
```csharp
public class LanguageTextRepository : EfCoreRepository<AbpDynamicResourceManagementDbContext, LanguageText, Guid>, ILanguageTextRepository
{
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
```
Bu sınıf, veritabanı üzerinden dil verilerine ulaşmak için gerekli temel metodları sağlar.

## 🧩 ABP’ye Entegrasyon: DynamicLocalizationResourceContributor
```csharp
public class DynamicLocalizationResourceContributor : ILocalizationResourceContributor
{
    private ILanguageTextRepository _languageTextRepository;

    public void Initialize(LocalizationResourceInitializationContext context)
    {
        _languageTextRepository = context.ServiceProvider.GetRequiredService<ILanguageTextRepository>();
    }

    public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        var texts = AsyncHelper.RunSync(() => _languageTextRepository.GetListByCultureNameAsync(cultureName));
        foreach (var item in texts)
        {
            dictionary[item.Name] = new LocalizedString(item.Name, item.Value);
        }
    }

    public async Task FillAsync(string cultureName, Dictionary<string, LocalizedString> dictionary)
    {
        var texts = await _languageTextRepository.GetListByCultureNameAsync(cultureName);
        foreach (var item in texts)
        {
            dictionary[item.Name] = new LocalizedString(item.Name, item.Value);
        }
    }

    public LocalizedString? GetOrNull(string cultureName, string name)
    {
        var item = AsyncHelper.RunSync(() => _languageTextRepository.GetdByCultureNameAndNameAsync(cultureName, name));
        return item == null ? null : new LocalizedString(name, item.Value);
    }

    public async Task<IEnumerable<string>> GetSupportedCulturesAsync()
    {
        return (await _languageTextRepository.GetListAsync()).Select(x => x.CultureName).Distinct();
    }
}
```

Bu sınıf, ABP’nin localization altyapısına bağlanarak .resx yerine veritabanından verileri alır.

## ⚙️ Modül Yapılandırması
```csharp
public class AbpDynamicResourceManagementDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));

            options.GlobalContributors.Add<DynamicLocalizationResourceContributor>();
        });
    }
}
```
GlobalContributors koleksiyonuna DynamicLocalizationResourceContributor eklenerek sistemde kullanılacak lokalizasyon sağlayıcısı tanıtılır.

## 🧪 Kullanım Örneği
Bir sayfada lokalize bir mesaj göstermek istediğinizde ABP’nin sağladığı IStringLocalizer<T> ya da IStringLocalizerFactory kullanılabilir:

```csharp
public class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        var message = _localizer["HelloMessage"];
        return View("Index", message);
    }
}
```

## 🧩 Çok Kiracılı (Multi-Tenant) Destek
Her iki entity (Language, LanguageText) IMultiTenant arayüzünü uyguladığı için ABP’nin tenant filtreleme altyapısından otomatik olarak faydalanır.

## 📌 Sonuç
Bu yapı sayesinde:
- .json dosyalarına bağlı kalmadan içerikleri dinamik olarak yönetebilirsiniz.
- Yönetici paneli üzerinden canlı dil güncellemeleri yapabilirsiniz.
- Her tenant için farklı dil çevirileri sunabilirsiniz.

ABP Framework’ün sunduğu altyapı ile birlikte dinamik lokalizasyon, özellikle SaaS mimarilerinde çok güçlü bir özelliktir.

