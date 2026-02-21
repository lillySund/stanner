using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages;

public class PortalModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public PortalModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public UserProfile? UserProfile { get; set; }
    public List<Subject> Subjects { get; set; } = new();
    public int TotalNotes { get; set; }
    public int TotalFlashcards { get; set; }
    public int FlashcardsStudiedToday { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Replace with actual user authentication
        string userId = "temp-user-1";
        
        // Get or create user profile
        UserProfile = await _context.UserProfiles
            .Include(u => u.ActiveSkin)
            .FirstOrDefaultAsync(u => u.UserId == userId);
            
        if (UserProfile == null)
        {
            UserProfile = new UserProfile
            {
                UserId = userId,
                DisplayName = "Student",
                TotalXP = 0,
                Level = 1
            };
            _context.UserProfiles.Add(UserProfile);
            await _context.SaveChangesAsync();
        }
        
        // Load subjects with related data
        Subjects = await _context.Subjects
            .Where(s => s.UserId == userId)
            .Include(s => s.Notes)
            .Include(s => s.Flashcards)
            .OrderBy(s => s.Name)
            .ToListAsync();
            
        // Calculate statistics
        TotalNotes = Subjects.Sum(s => s.Notes.Count);
        TotalFlashcards = Subjects.Sum(s => s.Flashcards.Count);
        FlashcardsStudiedToday = await _context.Flashcards
            .Where(f => f.Subject!.UserId == userId && f.LastStudied.HasValue && f.LastStudied.Value.Date == DateTime.UtcNow.Date)
            .CountAsync();
        
        return Page();
    }
}
