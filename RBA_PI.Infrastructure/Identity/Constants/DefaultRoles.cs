using Microsoft.AspNetCore.Identity;

namespace RBA_PI.Infrastructure.Identity.Constants
{
    public class DefaultRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = [RoleConstants.Admininistrador, RoleConstants.Cliente];

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
