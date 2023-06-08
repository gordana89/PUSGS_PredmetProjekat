using OrdersMicroservice.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersMicroservice.Domain.Abstractions
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> Delete(int id);
        Task<ProductDto> GetById(int id);
        Task<ProductDto> Add(ProductDto productDto);
        Task<ProductDto> Edit(int id, ProductDto productDto);

    }
}
