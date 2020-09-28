using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Entities.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int productId);

        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}
