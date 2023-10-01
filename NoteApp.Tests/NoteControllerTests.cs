using Microsoft.AspNetCore.Mvc;
using Moq;
using NoteApp.Controllers;
using NoteApp.Interfaces;
using NoteApp.Models;

namespace NoteApp.UnitTests
{
    [TestClass]
    public class NoteControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsListOfNotes()
        {
            // Arrange: Create a mock of the INote service with predefined notes
            var mockNoteService = new Mock<INote>();
            var notes = new List<Note> { new Note { Id = 1, Text = "Note 1" }, new Note { Id = 2, Text = "Note 2" } };
            mockNoteService.Setup(service => service.GetNotes()).Returns(notes);

            // Create an instance of the NoteController with the mock service
            var controller = new NoteController(mockNoteService.Object);
            
            // Act: Call the Get method on the controller
            var result = await controller.Get();

            // Assert: Verify that the result is not null and contains the expected number of notes
            var model = result as IEnumerable<Note>;
            Assert.IsNotNull(model);
            Assert.AreEqual(notes.Count, model.Count());
        }

        [TestMethod]
        public void Get_ReturnsNoteById()
        {
            // Arrange: Create a mock of the INote service with a predefined note
            var mockNoteService = new Mock<INote>();
            var note = new Note { Id = 1, Text = "Note 1" };
            mockNoteService.Setup(service => service.GetNoteById(1)).Returns(note);

            // Create an instance of the NoteController with the mock service
            var controller = new NoteController(mockNoteService.Object);

            // Act: Call the Get method on the controller
            var result = controller.Get(1);

            // Assert: Verify that the result is an OkObjectResult containing the expected note
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var model = okResult.Value as Note;
            Assert.IsNotNull(model);
            Assert.AreEqual(note.Id, model.Id);
        }

        [TestMethod]
        public void Post_AddNote()
        {
            // Arrange: Create a mock of the INote service
            var mockNoteService = new Mock<INote>();

            // Create an instance of the NoteController with the mock service
            var controller = new NoteController(mockNoteService.Object);
            var noteToAdd = new Note { Text = "New Note" };

            // Act: Call the Post method on the controller to add a note
            controller.Post(noteToAdd);

            // Assert: Verify that the AddNote method was called once with the expected note
            mockNoteService.Verify(service => service.AddNote(It.IsAny<Note>()), Times.Once);
            mockNoteService.Verify(service => service.AddNote(noteToAdd), Times.Once);
        }

        [TestMethod]
        public void Put_UpdatesNote()
        {
            // Arrange: Create a mock of the INote service
            var mockNoteService = new Mock<INote>();

            // Create an instance of the NoteController with the mock service
            var controller = new NoteController(mockNoteService.Object);
            var noteToUpdate = new Note { Id = 1, Text = "Updated Note" };

            // Act: Call the Put method on the controller to update a note
            controller.Put(noteToUpdate);

            // Assert: Verify that the UpdateNote method was called once with the expected note
            mockNoteService.Verify(service => service.UpdateNote(It.IsAny<Note>()), Times.Once);
            mockNoteService.Verify(service => service.UpdateNote(noteToUpdate), Times.Once);
        }
    }
}