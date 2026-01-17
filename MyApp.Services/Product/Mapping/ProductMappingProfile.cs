using AutoMapper;
using MyApp.Services.Product.DTOs;

namespace MyApp.Services.Product.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Data.Product.Entities.Product, ProductDto>();
            CreateMap<CreateProductDto, Data.Product.Entities.Product>();
            CreateMap<UpdateProductDto, Data.Product.Entities.Product>();
        }
    }
}
