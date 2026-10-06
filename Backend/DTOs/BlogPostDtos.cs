namespace Backend.DTOs;

public record BlogPostDto (
    int ID,
    string Title,
    string Content,
    DateTime PublishedDate
);

public record CreateBlogPostDto(
    string Title,
    string Content,
    DateTime PublishedDate
);

public record UpdateBlogPost(
    string Title,
    string Content
);