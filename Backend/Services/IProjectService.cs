using Backend.DTOs;

namespace Backend.Services;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectDTO>> GetAllAsync (CancellationToken ct = default);
    Task<ProjectDTO?> GetByIdAsync (int id, CancellationToken ct = default);
    Task<ProjectDTO> CreateAsync (CreateProjectDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync (int id, UpdateProjectDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync (int id, CancellationToken ct = default);
}