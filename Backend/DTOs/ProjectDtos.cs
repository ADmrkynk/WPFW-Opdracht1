namespace Backend.DTOs;

public record ProjectDTO (
    int ID,
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
    int ID,
    string Title,
    String Content,
    DateTime PublishedDate
);

