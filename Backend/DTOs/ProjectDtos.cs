namespace Backend.DTOs;

public record ProjectDto(
    int Id,
    string Title,
    string Description,
    string Category,
    string GitHubUrl,
    DateTime CreatedDate
);

// Backward-compatibility alias
public record ProjectDTO(int Id, string Title, string Description, string Category, string GitHubUrl, DateTime CreatedDate) 
    : ProjectDto(Id, Title, Description, Category, GitHubUrl, CreatedDate);

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

