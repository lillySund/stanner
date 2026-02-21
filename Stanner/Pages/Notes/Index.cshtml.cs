using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages.Notes;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Subject Subject { get; set; } = default!;
    public List<Note> Notes { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? subjectId)
    {
        if (subjectId == null)
        {
            return NotFound();
        }

        var subject = await _context.Subjects
            .Include(s => s.Notes)
                .ThenInclude(n => n.Attachments)
            .FirstOrDefaultAsync(s => s.Id == subjectId);

        if (subject == null)
        {
            return NotFound();
        }

        Subject = subject;
        Notes = subject.Notes.OrderByDescending(n => n.CreatedAt).ToList();

        return Page();
    }
}