using Backend.DTOs;
using Backend.Services;

namespace Backend.Endpoints;

public static class ProjectEndpoints
{
    public static RouteGroupBuilder MapProjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/projects").WithTags("Projects");

        group.MapGet("", async (IProjectService service, CancellationToken ct) =>
        {
            var projects = await service.GetAllAsync(ct);
            return TypedResults.Ok(projects);
        })
        .WithName("GetAllProjects");

        group.MapGet("/{id:int}", async (int id, IProjectService service, CancellationToken ct) =>
        {
            var project = await service.GetByIdAsync(id, ct);
            return project is not null
                ? TypedResults.Ok(project)
                : (IResult)TypedResults.NotFound();
        })
        .WithName("GetProjectById");

        group.MapPost("", async (CreateProjectDto dto, IProjectService service, CancellationToken ct) =>
        {
            var created = await service.CreateAsync(dto, ct);
            return TypedResults.CreatedAtRoute(created, "GetProjectById", new { id = created.Id });
        })
        .WithName("CreateProject");

        group.MapPut("/{id:int}", async (int id, UpdateProjectDto dto, IProjectService service, CancellationToken ct) =>
        {
            var updated = await service.UpdateAsync(id, dto, ct);
            return updated
                ? TypedResults.NoContent()
                : (IResult)TypedResults.NotFound();
        })
        .WithName("UpdateProject");        

        group.MapDelete("/{id:int}", async (int id, IProjectService service, CancellationToken ct) =>
        {
            var deleted = await service.DeleteAsync(id, ct);
            return deleted
                ? TypedResults.NoContent()
                :(IResult) TypedResults.NotFound();
        })
        .WithName("DeleteProject");

        return group;
    }
}