using Backend.DTOs;
namespace Backend.Services;

public interface IBlogPostService
{
    Task<IReadOnlyList<BlogPostDto>> GetAllAsync(CancellationToken ct = default);
    Task<BlogPostDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<BlogPostDto> CreateAsync(CreateBlogPostDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    
}