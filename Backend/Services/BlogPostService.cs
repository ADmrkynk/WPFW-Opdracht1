using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class BlogPostService : IBlogPostService
{
    private readonly PortfolioDb _db;
    public BlogPostService(PortfolioDb db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<BlogPostDto>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.BlogPosts
            .AsNoTracking()
            .Select(b => new BlogPostDto(b.Id, b.Title, b.Content, b.PublishedDate))
            .ToListAsync(ct);
    }

    public async Task<BlogPostDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.BlogPosts
            .AsNoTracking()
            .Where(b => b.Id == id)
            .Select(b => new BlogPostDto(b.Id, b.Title, b.Content, b.PublishedDate))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<BlogPostDto> CreateAsync(CreateBlogPostDto dto, CancellationToken ct = default)
    {
        var blogPost = new BlogPost
        {
            Title = dto.Title,
            Content = dto.Content,
            PublishedDate = DateTime.UtcNow
        };

        _db.BlogPosts.Add(blogPost);
        await _db.SaveChangesAsync(ct);

        return new BlogPostDto(blogPost.Id, blogPost.Title, blogPost.Content, blogPost.PublishedDate);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto, CancellationToken ct = default)
    {
        var blogPost = await _db.BlogPosts.FirstOrDefaultAsync(b => b.Id == id, ct);
        if (blogPost is null)
        {
            return false;
        }

        blogPost.Title = dto.Title;
        blogPost.Content = dto.Content;

        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var blogPost = await _db.BlogPosts.FirstOrDefaultAsync(b => b.Id == id, ct);
        if (blogPost is null)
        {
            return false;
        }

        _db.BlogPosts.Remove(blogPost);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}