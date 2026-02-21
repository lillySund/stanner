namespace Stanner.Models;

public class UserProfile
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public string? DisplayName { get; set; }
    public int TotalXP { get; set; } = 0;
    public int Level { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // navigatiojn
    public virtual ICollection<UnlockedSkin> UnlockedSkins { get; set; } = new List<UnlockedSkin>();
    public int? ActiveSkinId { get; set; }
    public virtual FlashcardSkin? ActiveSkin { get; set; }
    
    // xp logicx
    public void AddXP(int xp)
    {
        TotalXP += xp;
        UpdateLevel();
    }
    
    private void UpdateLevel()
    {
        // xp that is required at each level, can be referenced at levels
        Level = (TotalXP / 100) + 1;
    }
}