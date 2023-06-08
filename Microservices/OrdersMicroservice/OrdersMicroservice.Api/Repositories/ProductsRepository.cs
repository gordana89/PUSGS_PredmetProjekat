using OrdersMicroservice.Domain.Abstractions;
using OrdersMicroservice.Domain.Context;
using OrdersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OrdersMicroservice.Api.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _dataContext;
        public ProductsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dataContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _dataContext.Products.FindAsync(id);
        }

        public async Task<Product> Add(Product product)
        {
            var data = await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
            
            return data.Entity;
        }

        public async Task<Product> Edit(Product product)
        {
            var productToUpdate = await _dataContext.Products.FindAsync(product.Id);
            productToUpdate.Name = product.Name;
            productToUpdate.Components = product.Components;
            productToUpdate.Price = product.Price;
            var data = _dataContext.Update(productToUpdate);
            await _dataContext.SaveChangesAsync();
            
            return data.Entity;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await _dataContext.Products.FindAsync(id);
            
            var data = _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();

            return data.Entity;
        }
    }
}
