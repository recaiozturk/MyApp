using AutoMapper;
using MyApp.Core.DTOs;
using MyApp.Core.Entities;

namespace MyApp.Services.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}

