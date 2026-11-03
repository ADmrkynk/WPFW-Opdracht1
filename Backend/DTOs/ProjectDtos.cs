namespace Backend.DTOs;

public record ProjectDto(
    int Id,
    string Title,
    string Description,
    string Category,
    string GitHubUrl,
    DateTime CreatedDate
);

public record CreateProjectDto(
    string Title,
    string Description,
    string Category,
    string GitHubUrl
);

public record UpdateProjectDto (
    string Title,
    string Description,
    string Category,
    string GitHubUrl
);

