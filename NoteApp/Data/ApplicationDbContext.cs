using Microsoft.EntityFrameworkCore;
using NoteApp.ConfigureClasses;
using NoteApp.Models;

namespace NoteApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Note> Notes => Set<Note>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NoteConfiguration());
        }
    }
}
