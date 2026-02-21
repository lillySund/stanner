using Microsoft.EntityFrameworkCore;
using Stanner.Models;

namespace Stanner.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<FlashcardSkin> FlashcardSkins { get; set; }
    public DbSet<UnlockedSkin> UnlockedSkins { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // configuiring relationships, add??
        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Notes)
            .WithOne(n => n.Subject)
            .HasForeignKey(n => n.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Flashcards)
            .WithOne(f => f.Subject)
            .HasForeignKey(f => f.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Note>()
            .HasMany(n => n.Attachments)
            .WithOne(a => a.Note)
            .HasForeignKey(a => a.NoteId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<UserProfile>()
            .HasMany(u => u.UnlockedSkins)
            .WithOne(us => us.UserProfile)
            .HasForeignKey(us => us.UserProfileId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // default skins
        // having xp based system allows for more motiviation and higer chances with studyuing
        modelBuilder.Entity<FlashcardSkin>().HasData(
            new FlashcardSkin { Id = 1, Name = "Classic", Description = "Default flashcard style", XPCost = 0, CssClass = "skin-classic", IsDefault = true },
            new FlashcardSkin { Id = 2, Name = "Ocean Blue", Description = "Calming blue theme", XPCost = 100, CssClass = "skin-ocean" },
            new FlashcardSkin { Id = 3, Name = "Forest Green", Description = "Natural green theme", XPCost = 150, CssClass = "skin-forest" },
            new FlashcardSkin { Id = 4, Name = "Sunset Orange", Description = "Warm orange theme", XPCost = 200, CssClass = "skin-sunset" },
            new FlashcardSkin { Id = 5, Name = "Purple Galaxy", Description = "Cosmic purple theme", XPCost = 300, CssClass = "skin-galaxy" }
        );
    }
}