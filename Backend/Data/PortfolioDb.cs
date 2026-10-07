using System.Buffers.Text;
using System.Dynamic;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public sealed class PortfolioDb : DbContext
{
    public PortfolioDb(DbContextOptions<PortfolioDb> options) : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
}


