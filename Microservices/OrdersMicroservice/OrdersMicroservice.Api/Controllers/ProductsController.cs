using OrdersMicroservice.Domain.Abstractions;
using OrdersMicroservice.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OrdersMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMyLogger _myLogger;
        public ProductsController(IProductsService productsService, IMyLogger myLogger)
        {
            _productsService = productsService;
            _myLogger = myLogger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            _myLogger.LogInfo($"Get all products {DateTime.Now}");
           var products = await _productsService.GetAll();
            
           return Ok(products);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            _myLogger.LogInfo($"Get product by id: {id} {DateTime.Now}");

            var product = await _productsService.GetById(id);
            if(product == null)
            {
                return NotFound("Product doesn't exists");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDTO)
        {
            _myLogger.LogInfo($"Create new product {DateTime.Now}");

            return await _productsService.Add(productDTO);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id,ProductDto productDTO)
        {
            _myLogger.LogInfo($"Editing product with Id: {id} {DateTime.Now}");

            if (id != productDTO.Id)
                return BadRequest("Id in url and in body doesn't exists");

            return await _productsService.Edit(id,productDTO);
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            _myLogger.LogInfo($"Deleting product with Id:{id} {DateTime.Now}");

            return await _productsService.Delete(id);
        }
    }
}
