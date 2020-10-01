using System.Runtime.InteropServices;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecifications(string sort, int? typeId, int? brandId)
        : base(
            x => (!typeId.HasValue || x.ProductTypeId == typeId.Value) && 
                 (!brandId.HasValue || x.ProductBrandId == brandId.Value))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

            if (string.IsNullOrWhiteSpace(sort))
            {
                AddOrderByExpression(p => p.Name);
            }
            else
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderByExpression(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescendingExpression(p => p.Price);
                        break;
                    default:
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpecifications(int productId) : base(p => p.Id == productId)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
