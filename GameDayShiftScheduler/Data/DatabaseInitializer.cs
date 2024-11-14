
using Microsoft.AspNetCore.Identity;

namespace GameDayShiftScheduler.Data
{
    public class DatabaseInitializer
    {
        public static string[] roles = { "Admin", "TeamMember" };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                
                if (!roleExists)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }   
        }
    }
}
