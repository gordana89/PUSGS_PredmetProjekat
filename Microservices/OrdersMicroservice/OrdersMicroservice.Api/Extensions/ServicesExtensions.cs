using OrdersMicroservice.Domain.Abstractions;
using OrdersMicroservice.Domain.Context;
using OrdersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersMicroservice.Api.Repositories;
using OrdersMicroservice.Api.Services;

namespace OrdersMicroservice.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("Connection2")));
            
            services.AddCors();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IMailService, MailService>();
        }
    }
}
