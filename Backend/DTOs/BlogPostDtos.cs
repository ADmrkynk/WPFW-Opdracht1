namespace Backend.DTOs;

public record BlogPostDto(
    int Id,
    string Title,
    string Content,
    DateTime PublishedDate
);

public record CreateBlogPostDto(
    string Title,
    string Content
);

public record UpdateBlogPostDto(
    string Title,
    string Content
);