namespace Stanner.Models;

public class Note
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastModified { get; set; }
    
    // foreign
    public int SubjectId { get; set; }
    
    // navigagtions
    public virtual Subject? Subject { get; set; }
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}