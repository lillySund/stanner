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
    
    public string ErrorMessage { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            // name must be provided so required
            if (string.IsNullOrWhiteSpace(Subject?.Name))
            {
                ErrorMessage = "Subject name is required.";
                return Page();
            }

            // setting a user id
            Subject.UserId = "temp-user-1";
            
            // adding to database
            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();

            // redirecting go the portal
            return RedirectToPage("/Portal");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error creating subject: {ex.Message}";
            return Page();
        }
    }
}