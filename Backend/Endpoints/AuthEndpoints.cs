using System.Security.Claims;
using Backend.Auth;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var auth = app.MapGroup("/auth").WithTags("Auth");

        auth.MapPost("/register", async (
            RegisterRequest request,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager) =>
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Results.BadRequest(new { message = "E-mail en wachtwoord zijn verplicht." });
            }

            if (!await roleManager.RoleExistsAsync(Roles.Gast))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Gast));
            }

            var user = new User
            {
                UserName = request.Email.Trim(),
                Email = request.Email.Trim()
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Results.BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, Roles.Gast);

            return Results.Created("/auth/me", new { email = user.Email, role = Roles.Gast });
        });

        auth.MapPost("/login", async (
            LoginRequest request,
            SignInManager<User> signInManager) =>
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Results.BadRequest(new { message = "E-mail en wachtwoord zijn verplicht." });
            }

            var result = await signInManager.PasswordSignInAsync(
                request.Email.Trim(),
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new { message = "Ingelogd." });
        });

        auth.MapPost("/logout", async (SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok(new { message = "Uitgelogd." });
        }).RequireAuthorization();

        auth.MapGet("/me", (ClaimsPrincipal user) =>
        {
            var roles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            return Results.Ok(new
            {
                name = user.Identity?.Name,
                roles
            });
        }).RequireAuthorization();
    }
}
