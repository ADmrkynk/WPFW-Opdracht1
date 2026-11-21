using Backend.Auth;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        await using var scope = services.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        string[] roles = [Roles.Gast, Roles.Editor, Roles.Admin];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Optioneel: lokaal Editor- en Admin-account aanmaken als ze in user-secrets staan
        await SeedUserIfConfiguredAsync(userManager, configuration["Seed:EditorEmail"], configuration["Seed:EditorPassword"], Roles.Editor);
        await SeedUserIfConfiguredAsync(userManager, configuration["Seed:AdminEmail"], configuration["Seed:AdminPassword"], Roles.Admin);
    }

    private static async Task SeedUserIfConfiguredAsync(
        UserManager<User> userManager,
        string? email,
        string? password,
        string role)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                return;
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}

