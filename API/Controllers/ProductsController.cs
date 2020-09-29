using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        public ProductsController(IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<Product> productRepo, IGenericRepository<ProductType> productTypeRepo)
        {
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var productSpecifications = new ProductWithTypeAndBrandSpecifications();


            var products = await _productRepo.GetAllWithSpecifications(productSpecifications);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var productSpecification = new ProductWithTypeAndBrandSpecifications(productId);
            var product = await _productRepo.GetByIdWithSpecifications(productSpecification);

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _productBrandRepo.GetAllAsync();

            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllProductTypes()
        {
            var types = await _productTypeRepo.GetAllAsync();

            return Ok(types);
        }
    }
}
