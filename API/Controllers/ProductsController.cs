using API.Dtos;
using API.Error;
using API.PaginationDetails;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductQueryParameters parameters)
        {

            var productSpecifications = new ProductWithTypeAndBrandSpecifications(parameters);
            var numberOfProductsSpecifications = new ProductSpecificationToCountNumberOfProducts(parameters);


            var products = await _productRepo.GetAllWithSpecifications(productSpecifications);
            var totalItems = await _productRepo.CountNumberOfProducts(numberOfProductsSpecifications);

            var productsToReturn = _mapper.Map<IReadOnlyCollection<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>
            {
                Data = productsToReturn,
                Count = totalItems,
                PageSize = parameters.PageSize,
                PageIndex = parameters.PageIndex
            });
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var productSpecification = new ProductWithTypeAndBrandSpecifications(productId);
            var product = await _productRepo.GetByIdWithSpecifications(productSpecification);

            if (product == null)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            }

            var productToReturn = _mapper.Map<ProductToReturnDto>(product);

            return Ok(productToReturn);
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
