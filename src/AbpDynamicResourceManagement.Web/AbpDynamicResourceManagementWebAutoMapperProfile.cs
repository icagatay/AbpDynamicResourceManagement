using AutoMapper;
using AbpDynamicResourceManagement.Books;

namespace AbpDynamicResourceManagement.Web;

public class AbpDynamicResourceManagementWebAutoMapperProfile : Profile
{
    public AbpDynamicResourceManagementWebAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        
        //Define your object mappings here, for the Web project
    }
}
