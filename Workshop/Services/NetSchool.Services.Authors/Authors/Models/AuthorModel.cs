namespace NetSchool.Services.Authors;

using AutoMapper;
using NetSchool.Context.Entities;

public class AuthorModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string Country { get; set; }

}

public class AuthorModelProfile : Profile
{
    public AuthorModelProfile()
    {
        CreateMap<Author, AuthorModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Detail.Family))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Detail.Country))
            ;
    }
}