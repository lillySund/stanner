namespace Stanner.Models;

public class UnlockedSkin
{
    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public int SkinId { get; set; }
    public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
    
    // navigation
    public virtual UserProfile? UserProfile { get; set; }
    public virtual FlashcardSkin? Skin { get; set; }
}