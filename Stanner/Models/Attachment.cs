namespace Stanner.Models;

public class Attachment
{
    public int Id { get; set; }
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public required string FileType { get; set; } // PDF, PNG, JPEG
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // foreign keys if used in database
    public int NoteId { get; set; }
    
    // navigation
    public virtual Note? Note { get; set; }
}