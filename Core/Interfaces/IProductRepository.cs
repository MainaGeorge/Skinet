using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<IReadOnlyCollection<Product>> GetProductsAsync();
        Task<IReadOnlyCollection<ProductBrand>> GetAllProductBrandsAsync();
        Task<IReadOnlyCollection<ProductType>> GetAllProductTypesAsync();
    }
}
