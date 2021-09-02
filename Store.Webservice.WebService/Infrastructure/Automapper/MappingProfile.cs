using AutoMapper;
using Store.Webservice.Application.DTOs.Requests;
using Store.Webservice.Application.DTOs.Responses;
using Store.Webservice.Domain.Entities;

namespace Store.Webservice.WebService.Infrastructure.Automapper
{
    /// <summary>
    /// Contains the mapping profile and definitions.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class. Sets up the mapping profile.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Product, ProductItemResponse>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(source => source.ProductId))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(source => source.ProductName));

            CreateMap<AddProductRequest, Product>()
                .ForMember(dest => dest.ProductName, opt =>
                    opt.MapFrom(source => source.Name));
        }
    }
}