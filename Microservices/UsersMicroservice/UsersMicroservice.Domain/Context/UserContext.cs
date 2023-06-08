using UsersMicroservice.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace UsersMicroservice.Domain.Context
{
    public class UserContext : IdentityDbContext
    {
        public DbSet<User> ApplicationUsers { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
    }
}
