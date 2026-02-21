namespace Stanner.Models;

public class Subject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? CurrentGrade { get; set; }
    public string? TargetGrade { get; set; }
    public string? Color { get; set; } // ui customisation
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // foreign keys
    public required string UserId { get; set; }
    
    // navigation
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    public virtual ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
}