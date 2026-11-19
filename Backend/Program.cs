using Backend.Data;
using Backend.Endpoints;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PortfolioDb>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<PortfolioDb>();
try {
    db.Database.Migrate();
} catch (Exception ex) {
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Kon geen verbinding maken met SQL Server tijdens Migrate. Controleer de connection string in appsettings.json.");
}

// Swagger UI configureren
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapProjectEndpoints();
app.MapBlogPostEndpoints();

app.Run();