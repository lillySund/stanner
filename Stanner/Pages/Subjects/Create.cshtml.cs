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
            // Check if Name is provided (required field)
            if (string.IsNullOrWhiteSpace(Subject?.Name))
            {
                ErrorMessage = "Subject name is required.";
                return Page();
            }

            // Set user ID
            Subject.UserId = "temp-user-1";
            
            // Add to database
            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();

            // Redirect to Portal
            return RedirectToPage("/Portal");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error creating subject: {ex.Message}";
            return Page();
        }
    }
}