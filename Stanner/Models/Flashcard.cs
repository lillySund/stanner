namespace Stanner.Models;

public class Flashcard
{
    public int Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int TimesStudied { get; set; } = 0;
    public DateTime? LastStudied { get; set; }
    
    // foreign Key
    public int SubjectId { get; set; }
    
    // navigation
    public virtual Subject? Subject { get; set; }
}