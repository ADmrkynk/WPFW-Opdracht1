using Backend.DTOs;
using Backend.Services;

namespace Backend.Endpoints;

public static class BlogPostEndpoints
{
    public static RouteGroupBuilder MapBlogPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/blogposts").WithTags("BlogPosts");

        group.MapGet("", async (IBlogPostService service, CancellationToken ct) =>
        {
            var blogPosts = await service.GetAllAsync(ct);
            return TypedResults.Ok(blogPosts);
        })
        .WithName("GetAllBlogPosts");

        group.MapGet("/{id:int}", async (int id, IBlogPostService service, CancellationToken ct) =>
        {
            var post = await service.GetByIdAsync(id, ct);
            return post is not null
                ? TypedResults.Ok(post)
                : (IResult)TypedResults.NotFound();
        })
        .WithName("GetBlogPostById");

        group.MapPost("", async (CreateBlogPostDto dto, IBlogPostService service, CancellationToken ct) =>
        {
            var created = await service.CreateAsync(dto, ct);
            return TypedResults.CreatedAtRoute(created, "GetBlogPostById", new { id = created.Id });
        })
        .WithName("CreateBlogPost");

        group.MapPut("/{id:int}", async (int id, UpdateBlogPostDto dto, IBlogPostService service, CancellationToken ct) =>
        {
            var updated = await service.UpdateAsync(id, dto, ct);
            return updated 
                ? TypedResults.NoContent() 
                : (IResult)TypedResults.NotFound();
        })
        .WithName("UpdateBlogPost");

        group.MapDelete("/{id:int}", async (int id, IBlogPostService service, CancellationToken ct) =>
        {
            var deleted = await service.DeleteAsync(id, ct);
            return deleted 
                ? TypedResults.NoContent() 
                : (IResult)TypedResults.NotFound();
        })
        .WithName("DeleteBlogPost");

        return group;
    }
}