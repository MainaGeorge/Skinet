using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductMappings :Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(m => m.ProductBrand,
                    opt => opt.MapFrom(m => m.ProductBrand.Name))
                .ForMember(m => m.ProductType,
                    opt => opt.MapFrom(m => m.ProductType.Name))
                .ForMember(m => m.PictureUrl,
                    o => o.MapFrom<ProductImageUrlResolver>());
        }
    }
}
