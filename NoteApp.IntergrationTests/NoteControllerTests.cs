using Microsoft.AspNetCore.Mvc;
using Moq;
using NoteApp.Controllers;
using NoteApp.Interfaces;
using NoteApp.Models;
using NUnit.Framework;
using Moq.Language;
using Moq.Language.Flow;
using Moq.Protected;

namespace NoteApp.IntergrationTests
{
    [TestFixture]
    public class NoteControllerTests
    {
        private NoteController _controller;
        private Mock<INote> _mockNoteService;

        [SetUp]
        public void Setup()
        {
            _mockNoteService = new Mock<INote>();
            _controller = new NoteController(_mockNoteService.Object);
        }

       
        [Test]
        public void Get_WithValidId_ReturnsNote()
        {
            // Arrange: Prepare test data and setup the mock service
            var noteId = 1;
            var note = new Note { Id = noteId, Title = "Test Note", Text = "Test Text" };
            
            _mockNoteService.Setup(service => service.GetNoteById(noteId)).Returns(note);

            // Act: Call the Get method of the controller
            var result =  _controller.Get(noteId);

            // Assert: Verify the response
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var value = okResult.Value as Note;
            Assert.IsNotNull(value);
            Assert.AreEqual(noteId, value.Id);
        }


        [Test]
        public void Get_ReturnsNotFound_WhenNoteNotFound()
        {
            // Arrange: Setup the mock service to return null when called
            _mockNoteService.Setup(service => service.GetNoteById(It.IsAny<int>())).Returns((Note)null);

            // Act: Call the Get method of the controller
            var result = _controller.Get(1);

            // Assert: Verify that a NotFoundResult is returned
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Get_ReturnsNote_WhenNoteFound()
        {
            // Arrange: Prepare a test note and setup the mock service
            var note = new Note { Id = 1, Title = "Note 1", Text = "Text 1" };
            _mockNoteService.Setup(service => service.GetNoteById(1)).Returns(note);

            // Act: Call the Get method of the controller
            var result = _controller.Get(1);

            // Assert: Verify that an OkObjectResult with a Note object is returned
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOf<Note>(okResult.Value);
        }

        [Test]
        public void Post_AddsNote()
        {
            // Arrange: Prepare a new note
            var note = new Note { Title = "New Note", Text = "New Text" };

            // Act: Call the Post method of the controller
            _controller.Post(note);

            // Assert: Verify that the AddNote method of the mock service is called once
            _mockNoteService.Verify(service => service.AddNote(note), Times.Once);
        }

        [Test]
        public void Put_UpdatesNote()
        {
            // Arrange: Prepare an updated note
            var note = new Note { Id = 1, Title = "Updated Note", Text = "Updated Text" };

            // Act: Call the Put method of the controller
            _controller.Put(note);

            // Assert: Verify that the UpdateNote method of the mock service is called once
            _mockNoteService.Verify(service => service.UpdateNote(note), Times.Once);
        }
    }
}