using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace UsersMicroservice.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseInitRoles(this IApplicationBuilder app, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "Administrator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Deliverer").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "Deliverer"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "Customer"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
