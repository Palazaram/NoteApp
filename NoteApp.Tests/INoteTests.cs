using Microsoft.AspNetCore.Routing;
using Moq;
using NoteApp.Interfaces;
using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.UnitTests
{
    [TestClass]
    public class INoteTests
    {
        [TestMethod]
        public void GetNotes_ReturnsListOfNotes()
        {
            // Arrange: Create a mock of the INote service with predefined notes
            var noteService = new Mock<INote>();
            var notes = new List<Note> { new Note { Id = 1, Title = "Title 1", Text = "Text 1" }, new Note { Id = 2, Title = "Title 2", Text = "Text 2" } };
            noteService.Setup(service => service.GetNotes()).Returns(notes);

            // Act: Call the GetNotes method on the mock service
            var result = noteService.Object.GetNotes();

            // Assert: Verify that the result is not null and contains the expected number of notes
            Assert.IsNotNull(result);
            Assert.AreEqual(notes.Count, result.Count());
        }

        [TestMethod]
        public void AddNote()
        {
            // Arrange: Create a mock of the INote service
            var noteService = new Mock<INote>();
            var noteToAdd = new Note { Title = "New Title", Text = "New Text" };

            // Act: Add a note using the mock service
            noteService.Object.AddNote(noteToAdd);

            // Assert: Verify that the AddNote method was called once with the expected note
            noteService.Verify(service => service.AddNote(It.IsAny<Note>()), Times.Once);
            noteService.Verify(service => service.AddNote(noteToAdd), Times.Once);
        }

        [TestMethod]
        public void UpdatesNote()
        {
            // Arrange: Create a mock of the INote service
            var noteService = new Mock<INote>();
            var noteToUpdate = new Note { Id = 1, Title = "Updated Title", Text = "Updated Text" };

            // Act: Update a note using the mock service
            noteService.Object.UpdateNote(noteToUpdate);

            // Assert: Verify that the UpdateNote method was called once with the expected note
            noteService.Verify(service => service.UpdateNote(It.IsAny<Note>()), Times.Once);
            noteService.Verify(service => service.UpdateNote(noteToUpdate), Times.Once);
        }

        [TestMethod]
        public void GetNoteById_ReturnsNote()
        {
            // Arrange: Create a mock of the INote service with a predefined note
            var noteService = new Mock<INote>();
            var note = new Note { Id = 1, Title = "Title 1", Text = "Text 1" };
            noteService.Setup(service => service.GetNoteById(1)).Returns(note);

            // Act: Call the GetNoteById method on the mock service
            var result = noteService.Object.GetNoteById(1);

            // Assert: Verify that the result is not null and contains the expected note data
            Assert.IsNotNull(result);
            Assert.AreEqual(note.Id, result.Id);
            Assert.AreEqual(note.Title, result.Title);
            Assert.AreEqual(note.Text, result.Text);
        }
    }
}
