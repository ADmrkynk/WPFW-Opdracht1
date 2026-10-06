namespace Backend.Models;

public class Project
{
    public int ID {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public string Category {get; set;} = string.Empty; 
    public string GitHubUrl {get; set;} = string.Empty;
    public DateTime CreatedDate {get; set;} = DateTime.UtcNow;
}