using Backend.DTOs;

namespace Backend.Services;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectDto>> GetAllAsync(CancellationToken ct = default);
    Task<ProjectDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ProjectDto> CreateAsync(CreateProjectDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, UpdateProjectDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}