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
    public class NoteManagerTests
    {
        private ApplicationDbContext _dbContext;
        private INote _noteService;

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
        public void AddNote_Should_AddNoteToDatabase()
        {
            // Arrange: Prepare a new note to add
            var noteToAdd = new Note
            {
                Title = "Test Title",
                Text = "Test Text",
                CreatedAt = DateTime.Now
            };

            // Remove any existing notes from the in-memory database
            _dbContext.Notes.RemoveRange(_dbContext.Notes);
            _dbContext.SaveChanges();

            // Act: Add the new note using the service
            _noteService.AddNote(noteToAdd);

            // Assert: Verify that the note was added to the database correctly
            var addedNote = _dbContext.Notes.FirstOrDefault();
            Assert.IsNotNull(addedNote, "Note was not added to the database");
            Assert.AreEqual(noteToAdd.Title, addedNote.Title);
            Assert.AreEqual(noteToAdd.Text, addedNote.Text);
            Assert.AreEqual(noteToAdd.CreatedAt, addedNote.CreatedAt);
        }

        [Test]
        public void GetNoteById_ExistingId_Should_ReturnNote()
        {
            // Arrange: Prepare an existing note and add it to the database
            var existingNote = new Note
            {
                Id = 2,
                Title = "Existing Title2",
                Text = "Existing Text2",
                CreatedAt = DateTime.Now
            };

            _dbContext.Notes.RemoveRange(_dbContext.Notes);
            _dbContext.SaveChanges();

            _dbContext.Notes.Add(existingNote);
            _dbContext.SaveChanges();

            // Act: Retrieve the note by its ID using the service
            var retrievedNote = _noteService.GetNoteById(2);

            // Assert: Verify that the retrieved note matches the expected values
            Assert.IsNotNull(retrievedNote);
            Assert.AreEqual(existingNote.Id, retrievedNote.Id);
            Assert.AreEqual(existingNote.Title, retrievedNote.Title);
            Assert.AreEqual(existingNote.Text, retrievedNote.Text);
            Assert.AreEqual(existingNote.CreatedAt, retrievedNote.CreatedAt);
        }

        [Test]
        public void GetNoteById_NonExistingId_Should_ThrowException()
        {
            // Act and Assert: Verify that an exception is thrown when attempting to retrieve a non-existing note
            Assert.Throws<ArgumentException>(() => _noteService.GetNoteById(5));
        }

        [Test]
        public void UpdateNote_ExistingNote_Should_UpdateNoteInDatabase()
        {
            // Arrange: Prepare an existing note and add it to the database
            var existingNote = new Note
            {
                Id = 1,
                Title = "Existing Title",
                Text = "Existing Text",
                CreatedAt = DateTime.Now
            };

            _dbContext.Notes.Add(existingNote);
            _dbContext.SaveChanges();

            // Create an updated note with the same ID
            var updatedNote = new Note
            {
                Id = 1,
                Title = "Updated Title",
                Text = "Updated Text",
                CreatedAt = DateTime.Now
            };

            // Act: Update the existing note using the service
            _noteService.UpdateNote(updatedNote);

            // Assert: Verify that the note in the database has been updated correctly
            var updatedNoteInDb = _dbContext.Notes.FirstOrDefault(n => n.Id == 1);
            Assert.IsNotNull(updatedNoteInDb, "Note was not found in the database");
            Assert.AreEqual(updatedNote.Id, updatedNoteInDb.Id);
            Assert.AreEqual(updatedNote.Title, updatedNoteInDb.Title);
            Assert.AreEqual(updatedNote.Text, updatedNoteInDb.Text);
            Assert.AreEqual(updatedNote.CreatedAt, updatedNoteInDb.CreatedAt);
        }

        [Test]
        public void UpdateNote_NonExistingNote_Should_ThrowException()
        {
            // Arrange: Prepare an updated note with a non-existing ID
            var updatedNote = new Note
            {
                Id = 10,
                Title = "Updated Title",
                Text = "Updated Text",
                CreatedAt = DateTime.Now
            };

            // Act and Assert: Verify that an exception is thrown when attempting to update a non-existing note
            Assert.Throws<ArgumentException>(() => _noteService.UpdateNote(updatedNote));
        }

        [Test]
        public void GetNotes_Should_ReturnListOfNotes()
        {
            // Arrange: Prepare a list of notes to add to the database
            var notesToAdd = new List<Note>
            {
                new Note
                {
                    Title = "Note 1",
                    Text = "Text 1",
                    CreatedAt = DateTime.Now
                },
                new Note
                {
                    Title = "Note 2",
                    Text = "Text 2",
                    CreatedAt = DateTime.Now
                }
            };

            // Remove any existing notes from the in-memory database
            _dbContext.Notes.RemoveRange(_dbContext.Notes);
            _dbContext.SaveChanges();

            // Add the prepared notes to the database
            _dbContext.Notes.AddRange(notesToAdd);
            _dbContext.SaveChanges();

            // Act: Retrieve the list of notes using the service
            var notes = _noteService.GetNotes().ToList();

            // Assert: Verify that the list of notes is not null and has the expected count
            Assert.IsNotNull(notes);
            Assert.AreEqual(notesToAdd.Count, notes.Count());
        }

    }
}
