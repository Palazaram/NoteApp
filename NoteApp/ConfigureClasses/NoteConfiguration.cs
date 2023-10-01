using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Models;

namespace NoteApp.ConfigureClasses
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title).IsRequired().IsUnicode().HasColumnType("VARCHAR(100)");
            builder.Property(n => n.Text).IsRequired().IsUnicode().HasColumnType("TEXT");
            builder.Property(n => n.CreatedAt).IsRequired().HasColumnType("timestamp");
        }
    }
}
