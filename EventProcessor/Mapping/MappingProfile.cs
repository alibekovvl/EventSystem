using AutoMapper;
using EventGenerator.Models; 
using EventProcessor.Models; 
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Incident, IncidentPr>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)) 
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time)) 
            .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events)); 
        
        CreateMap<Event, EventPr>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)) 
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
    }
}