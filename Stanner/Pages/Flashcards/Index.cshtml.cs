using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages.Flashcards;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
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

        Subject = await _context.Subjects.FindAsync(subjectId);

        if (Subject == null)
        {
            return NotFound();
        }

        Flashcards = await _context.Flashcards
            .Where(f => f.SubjectId == subjectId)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, int subjectId)
    {
        var flashcard = await _context.Flashcards.FindAsync(id);

        if (flashcard != null)
        {
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Flashcard/flashcards has/have been deleted";
        }

        return RedirectToPage(new { subjectId });
    }
}