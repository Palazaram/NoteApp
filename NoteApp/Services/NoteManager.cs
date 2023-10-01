using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Interfaces;
using NoteApp.Models;

namespace NoteApp.Services
{
    public class NoteManager : INote
    {
        private readonly ApplicationDbContext _db;

        public NoteManager(ApplicationDbContext db)
        {
            _db = db;
        }

        // Method for adding a note to the database.
        public void AddNote(Note note)
        {
            try
            {
                _db.Notes.Add(note);
                _db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        // Method for retrieving a note by its identifier.
        public Note GetNoteById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var note = _db.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                throw new ArgumentException("Note not found");
            }

            return note;
        }

        // Method for getting a list of all notes from the database.
        public IEnumerable<Note> GetNotes()
        {
            IEnumerable<Note> notes = (from note in _db.Notes
                                       select new Note
                                       {
                                           Id = note.Id,
                                           Title = note.Title,
                                           Text = note.Text,
                                           CreatedAt = note.CreatedAt
                                       }).ToList();
            return notes;
        }

        // Method for updating information about a note in the database.
        public void UpdateNote(Note note)
        {
            try
            {
                var existingNote = _db.Notes.Find(note.Id);

                if (existingNote != null)
                {
                    existingNote.Title = note.Title;
                    existingNote.Text = note.Text;
                    existingNote.CreatedAt = note.CreatedAt;

                    _db.Entry(existingNote).Property(x => x.Title).IsModified = true;
                    _db.Entry(existingNote).Property(x => x.Text).IsModified = true;
                    _db.Entry(existingNote).Property(x => x.CreatedAt).IsModified = true;

                    _db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Note not found");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
