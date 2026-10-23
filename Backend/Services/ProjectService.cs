using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class ProjectService : IProjectService
{
 private readonly PortfolioDb _db;

 public ProjectService (PortfolioDb db)
    {
        _db = db;
    }   

    public async Task<IReadOnlyList<ProjectDTO>> GetAllAsync (CancellationToken ct = default)
    {
        return await _db.Projects
        .AsNoTracking()
        .Select(p => new ProjectDTO(p.ID, p.Title, p.Description, p.Category, p.GitHubUrl, p.CreatedDate))
        .ToListAsync(ct);
    }

    public async Task<ProjectDTO?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.Projects
        .AsNoTracking()
        .Where (p => p.ID == id)
        .Select(p => new ProjectDTO (p.ID, p.Title, p.Description, p.Category, p.GitHubUrl, p.CreatedDate))
        .FirstOrDefaultAsync(ct);
    }

    public async Task<ProjectDTO> CreateAsync(CreateProjectDto dto, CancellationToken ct = default)
    {
        var project = new Project
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            GitHubUrl = dto.GitHubUrl,
            CreatedDate = DateTime.UtcNow
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync(ct);

        return new ProjectDTO(project.ID, project.Title, project.Description, project.Category, project.GitHubUrl, project.CreatedDate);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto, CancellationToken ct = default)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.ID == id, ct);
        if (project is null)
        {
            return false;
        }

        project.Title = dto.Title;
        project.Description = dto.Description;
        project.Category = dto.Category;
        project.GitHubUrl = dto.GitHubUrl;

        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.ID == id, ct);
        if (project is null)
        {
            return false;
        }

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}