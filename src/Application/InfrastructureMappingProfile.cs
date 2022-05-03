using Application.Commands;
using Application.Entities;
using Application.Models;
using AutoMapper;

namespace Application;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<AddProduct.Command, ProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Key, opt => opt.Ignore())
            .ForMember(dest => dest.InsertTime, opt => opt.Ignore());
        
        CreateMap<Photo, PhotoEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Key, opt => opt.Ignore())
            .ForMember(dest => dest.InsertTime, opt => opt.Ignore());
    }
}