using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must fill in the title")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The title must be between 1 and 100 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must fill in the text")]
        public string Text { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
