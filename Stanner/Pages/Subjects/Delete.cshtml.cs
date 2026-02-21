using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages.Subjects;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Subject Subject { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _context.Subjects
            .Include(s => s.Notes)
            .Include(s => s.Flashcards)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (subject == null)
        {
            return NotFound();
        }
        
        Subject = subject;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _context.Subjects.FindAsync(id);

        if (subject != null)
        {
            Subject = subject;
            _context.Subjects.Remove(Subject);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/Portal");
    }
}