using UsersMicroservice.Domain.Abstractions;
using UsersMicroservice.Domain.Context;
using UsersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersMicroservice.Api.Repositories;
using UsersMicroservice.Api.Services;

namespace UsersMicroservice.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection1")));

            services.AddDefaultIdentity<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<UserContext>();
            services.AddCors();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IMailService, MailService>();
        }
    }
}
