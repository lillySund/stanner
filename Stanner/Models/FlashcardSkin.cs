namespace Stanner.Models;

public class FlashcardSkin
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int XPCost { get; set; }
    public required string CssClass { get; set; } // css for styling
    public string? PreviewImage { get; set; }
    public bool IsDefault { get; set; } = false;
}   