using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecificationToCountNumberOfProducts : BaseSpecification<Product>
    {
        public ProductSpecificationToCountNumberOfProducts(ProductQueryParameters parameters) : base(
            x => (!parameters.TypeId.HasValue || x.ProductTypeId == parameters.TypeId.Value) &&
                 (!parameters.BrandId.HasValue || x.ProductBrandId == parameters.BrandId.Value) &&
                 (string.IsNullOrWhiteSpace(parameters.Search) || x.Name.ToLower().Contains(parameters.Search)))
        {
        }
    }
}
