using OrdersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersMicroservice.Domain.Abstractions
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAll ();
        Task<Product> GetById(int id);
        Task<Product> Add(Product product);
        Task<Product> Edit(Product product);
        Task<Product> Delete(int id);
    }
}
