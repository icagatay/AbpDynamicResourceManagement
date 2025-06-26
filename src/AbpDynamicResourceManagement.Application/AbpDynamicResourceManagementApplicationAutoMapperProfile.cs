using AutoMapper;
using AbpDynamicResourceManagement.Books;

namespace AbpDynamicResourceManagement;

public class AbpDynamicResourceManagementApplicationAutoMapperProfile : Profile
{
    public AbpDynamicResourceManagementApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
