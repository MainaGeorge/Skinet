using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecifications(ProductQueryParameters parameters)
        : base(
            x => (!parameters.TypeId.HasValue || x.ProductTypeId == parameters.TypeId.Value) &&
                 (!parameters.BrandId.HasValue || x.ProductBrandId == parameters.BrandId.Value) &&
                 (string.IsNullOrWhiteSpace(parameters.Search) || x.Name.ToLower().Contains(parameters.Search)))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            var itemsToSkip = parameters.PageSize * (parameters.PageIndex - 1);

            AddPagination(parameters.PageSize, itemsToSkip);

            if (string.IsNullOrWhiteSpace(parameters.Sort))
            {
                AddOrderByExpression(p => p.Name);
            }
            else
            {
                switch (parameters.Sort)
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
