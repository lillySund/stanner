using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stanner.Data;
using Stanner.Models;

namespace Stanner.Pages.Subjects;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Subject Subject { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Replace with actual user authentication
        Subject.UserId = "temp-user-1";
        
        _context.Subjects.Add(Subject);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Portal");
    }
}