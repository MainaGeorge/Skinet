using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecifications()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }

        public ProductWithTypeAndBrandSpecifications(int productId) : base(p => p.Id == productId)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
