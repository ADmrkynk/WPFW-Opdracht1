using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public sealed class PortfolioDb : IdentityDbContext<User>
{
    public PortfolioDb(DbContextOptions<PortfolioDb> options) : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
