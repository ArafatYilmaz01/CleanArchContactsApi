using AutoMapper;

namespace BP.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Data.Models.Contact, Models.ContactDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")).ReverseMap();
    }
}
