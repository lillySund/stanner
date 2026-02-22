using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;


//creating etc
namespace Stanner.Pages.Flashcards;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Flashcard Flashcard { get; set; } = default!;

    public Subject? Subject { get; set; }

    // new flashcards ids 
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

        Flashcard = new Flashcard
        {
            SubjectId = subjectId.Value,
            Question = string.Empty,
            Answer = string.Empty
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Subject = await _context.Subjects.FindAsync(Flashcard.SubjectId);
            return Page();
        }

        _context.Flashcards.Add(Flashcard);
        await _context.SaveChangesAsync();

        TempData["Message"] = "Flashcard created";
        return RedirectToPage("./Index", new { subjectId = Flashcard.SubjectId });
    }
}