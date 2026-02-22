using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages.Flashcards;

public class StudyModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public StudyModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Subject? Subject { get; set; }
    public List<Flashcard> Flashcards { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? subjectId)
    {
        if (subjectId == null)
        {
            return RedirectToPage("/Portal");
        }

        Subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == subjectId);

        if (Subject == null)
        {
            return NotFound();
        }

        Flashcards = await _context.Flashcards
            .Where(f => f.SubjectId == subjectId)
            .OrderBy(f => f.Id)
            .ToListAsync();

        if (!Flashcards.Any())
        {
            TempData["Message"] = "No flashcards are available for this subject.";
            return RedirectToPage("/Portal");
        }

        return Page();
    }
    //xp system adding 10 xp for each flashcard studied
    public async Task<IActionResult> OnPostCompleteStudyAsync(int flashcardId)
    {
        var flashcard = await _context.Flashcards.FindAsync(flashcardId);
        
        if (flashcard != null)
        {
            flashcard.TimesStudied++;
            flashcard.LastStudied = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return new JsonResult(new { success = true, xp = 10 });
    }
}
