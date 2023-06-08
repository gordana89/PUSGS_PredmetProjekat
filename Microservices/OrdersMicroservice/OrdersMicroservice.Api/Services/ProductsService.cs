using OrdersMicroservice.Domain.Abstractions;
using OrdersMicroservice.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrdersMicroservice.Api.Exceptions;
using OrdersMicroservice.Api.Extensions;

namespace OrdersMicroservice.Api.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ProductDto> Delete(int id)
        {
            var productExist = (await _productsRepository.GetById(id));
            
            if (productExist == null)
                throw new NotFoundException($"Product with id: {id} doesn't exist");
            
            return (await _productsRepository.Delete(id)).ToDto(); ;
        }

        public async Task<ProductDto> GetById(int id)
        {
           return (await _productsRepository.GetById(id)).ToDto();
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var data = await _productsRepository.GetAll();

            List<ProductDto> productsDTO = new List<ProductDto>();
            foreach (var item in data)
            {
                productsDTO.Add(item.ToDto());
            }
            
            return productsDTO;
        }

        public async Task<ProductDto> Add(ProductDto productDTO)
        {
            var data = await _productsRepository.Add(productDTO.ToEntity());
            
            return data.ToDto();
        }

        public async Task<ProductDto> Edit(int id, ProductDto productDTO)
        {
            var productExist = (await _productsRepository.GetById(id));
            if (productExist == null)
                throw new NotFoundException($"Product with id: {id} doesn't exist");

            var data = await _productsRepository.Edit(productDTO.ToEntity());

            return data.ToDto();
        }
    }
}
