using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages.Notes;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Subject Subject { get; set; } = default!;

    [BindProperty]
    public Note Note { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? subjectId)
    {
        if (subjectId == null)
        {
            return NotFound();
        }

        var subject = await _context.Subjects.FindAsync(subjectId);
        
        if (subject == null)
        {
            return NotFound();
        }

        Subject = subject;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int subjectId)
    {
        if (!ModelState.IsValid)
        {
            Subject = (await _context.Subjects.FindAsync(subjectId))!;
            return Page();
        }

        Note.SubjectId = subjectId;
        Note.CreatedAt = DateTime.UtcNow;
        
        _context.Notes.Add(Note);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Notes/Index", new { subjectId });
    }
}