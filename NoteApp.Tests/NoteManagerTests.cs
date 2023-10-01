using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using NoteApp.Data;
using NoteApp.Models;
using NoteApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.UnitTests
{
    [TestClass]
    public class NoteManagerTests
    {
        [TestMethod]
        public void AddNote_Should_Add_Note_To_Database()
        {
            // Arrange: Create an in-memory database context and a NoteManager instance
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var manager = new NoteManager(context);
                var note = new Note { Id = 1, Title = "Test Note", Text = "Test Text", CreatedAt = DateTime.Now };

                // Act: Add a note to the database using the manager
                manager.AddNote(note);

                // Assert: Verify that there is one note in the database
                Assert.AreEqual(1, context.Notes.Count());
            }
        }

        [TestMethod]
        public void GetNoteById_Should_Return_Note_If_It_Exists()
        {
            // Arrange: Create an in-memory database context and a NoteManager instance
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var manager = new NoteManager(context);
                var id = 1;

                // Ensure that there's no note with the specified ID in the database
                var existingNote = context.Notes.FirstOrDefault(n => n.Id == id);
                if (existingNote != null)
                {
                    context.Notes.Remove(existingNote);
                    context.SaveChanges();
                }

                var note = new Note { Id = 1, Title = "Test Note", Text = "Test Text", CreatedAt = DateTime.Now };
                context.Notes.Add(note);
                context.SaveChanges();

                // Act: Retrieve a note by ID using the manager
                var result = manager.GetNoteById(id);

                // Assert: Verify that the retrieved note matches the expected ID
                Assert.IsNotNull(result);
                Assert.AreEqual(id, result.Id);
            }
        }

        [TestMethod]
        public void GetNoteById_Should_Throw_Exception_If_Note_Not_Found()
        {
            // Arrange: Create an in-memory database context and a NoteManager instance
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var manager = new NoteManager(context);
                int? id = 100;

                // Act and Assert: Verify that an ArgumentException is thrown when attempting to retrieve a non-existent note
                Assert.ThrowsException<ArgumentException>(() => manager.GetNoteById(id));
            }
        }

        [TestMethod]
        public void GetNotes_Should_Return_All_Notes()
        {
            // Arrange: Create an in-memory database context and add multiple notes to it
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Clear the note list before adding new notes
                context.Notes.RemoveRange(context.Notes);

                context.Notes.AddRange(new List<Note>
                {
                    new Note { Id = 1, Title = "Note 1", Text = "Text 1", CreatedAt = DateTime.Now },
                    new Note { Id = 2, Title = "Note 2", Text = "Text 2", CreatedAt = DateTime.Now },
                    new Note { Id = 3, Title = "Note 3", Text = "Text 3", CreatedAt = DateTime.Now }
                });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var manager = new NoteManager(context);

                // Act: Retrieve all notes using the manager
                var result = manager.GetNotes();

                // Assert: Verify that the result contains the expected number of notes
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void UpdateNote_Should_Update_Note_In_Database()
        {
            // Arrange: Create an in-memory database context and add a note to it
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Notes.RemoveRange(context.Notes);
                var manager = new NoteManager(context);
                var note = new Note { Id = 1, Title = "Test Note", Text = "Test Text", CreatedAt = DateTime.Now };
                context.Notes.Add(note);
                context.SaveChanges();

                var updatedNote = new Note { Id = note.Id, Title = "Updated Note", Text = "Updated Text", CreatedAt = DateTime.Now };

                // Act: Update the note using the manager
                manager.UpdateNote(updatedNote);

                var noteFromDb = context.Notes.Find(note.Id);

                // Assert: Verify that the note in the database has been updated
                Assert.AreEqual("Updated Note", noteFromDb.Title);
                Assert.AreEqual("Updated Text", noteFromDb.Text);
            }
        }
    }
}
