using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Interfaces;
using NoteApp.Models;
using NoteApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.IntergrationTests
{
    [TestFixture]
    public class INoteTests
    {
        private INote _noteService;
        private ApplicationDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            _dbContext = new ApplicationDbContext(options);

            // Create an instance of the INote service for testing
            _noteService = new NoteManager(_dbContext);
        }

        [Test]
        public void GetNotes_ReturnsNotEmptyList()
        {
            // Test if GetNotes method returns a non-null and non-empty list of notes
            IEnumerable<Note> notes = _noteService.GetNotes();


            Assert.IsNotNull(notes, "Список заметок не должен быть null");
            Assert.IsNotEmpty(notes, "Список заметок не должен быть пустым");
        }

        [Test]
        public void AddNote_AddsNoteToList()
        {
            // Create a new note to add
            Note newNote = new Note
            {
                Title = "Заголовок новой заметки",
                Text = "Текст новой заметки"
            };

            // Add the new note
            _noteService.AddNote(newNote);

            // Retrieve the added note and check if it's not null, indicating that it was added successfully
            Note addedNote = _noteService.GetNoteById(newNote.Id);
            Assert.IsNotNull(addedNote, "Заметка не была добавлена");
        }

        [Test]
        public void UpdateNote_UpdatesNote()
        {
            // Create an existing note with known properties
            Note existingNote = new Note
            {
                Id = 2,
                Title = "Заголовок существующей заметки2",
                Text = "Текст существующей заметки2"
            };

            // Add the existing note
            _noteService.AddNote(existingNote);

            // Modify the title of the existing note
            existingNote.Title = "Updated Title";

            // Update the existing note
            _noteService.UpdateNote(existingNote);

            // Retrieve the updated note and check if it's not null, and if the title was updated as expected
            Note updatedNote = _noteService.GetNoteById(existingNote.Id);
            Assert.IsNotNull(updatedNote, "Заметка не была обновлена");
            Assert.AreEqual("Updated Title", updatedNote.Title, "Заголовок не был обновлен");
        }

        [Test]
        public void GetNoteById_ReturnsNote()
        {
            // Create a new note 
            Note newNote = new Note
            {
                Id = 3, 
                Title = "Заголовок новой заметки3",
                Text = "Текст новой заметки3"
            };

            // Add the new note
            _noteService.AddNote(newNote);

            // Retrieve the note by its ID and check if it's not null, and if the ID matches the expected value
            Note retrievedNote = _noteService.GetNoteById(newNote.Id);
            Assert.IsNotNull(retrievedNote, "Заметка не была найдена");
            Assert.AreEqual(newNote.Id, retrievedNote.Id, "Id заметки не совпадает");
        }
    }
}
