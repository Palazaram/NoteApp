using NoteApp.Models;

namespace NoteApp.Interfaces
{
    public interface INote
    {
        public IEnumerable<Note> GetNotes();
        public void AddNote(Note note);
        public void UpdateNote(Note note);
        public Note GetNoteById(int? id);
    }
}
