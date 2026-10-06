namespace Backend.Models;

public class BlogPost
{
    public int ID {get; set;}
    public string Title {get; set; } = string.Empty;
    public string Content {get; set;} = string.Empty;
    public DateTime PublishedDate {get; set; } = DateTime.UtcNow;
}