using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteApp.Models;

namespace NoteApp.UnitTests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void Title_IsRequired()
        {
            // Arrange: Create a Note instance without a title
            var note = new Note { Text = "Note Text" };

            // Act: Attempt to validate the Note instance
            var context = new ValidationContext(note, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, context, result, true);

            // Assert: Verify that the validation fails and reports the required title
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("You must fill in the title", result[0].ErrorMessage);
        }

        [TestMethod]
        public void Title_MinimumLength()
        {
            // Arrange: Create a Note instance with an empty title
            var note = new Note { Title = "", Text = "Note Text" };

            // Act: Attempt to validate the Note instance
            var context = new ValidationContext(note, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, context, result, true);

            // Assert: Verify that the validation fails and reports the required title
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("You must fill in the title", result[0].ErrorMessage);
        }

        [TestMethod]
        public void Title_MaximumLength()
        {
            // Arrange: Create a Note instance with a title exceeding the maximum length
            var note = new Note { Title = new string('A', 101), Text = "Note Text" };

            // Act: Attempt to validate the Note instance
            var context = new ValidationContext(note, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, context, result, true);

            // Assert: Verify that the validation fails and reports the title length constraint
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("The title must be between 1 and 100 characters long", result[0].ErrorMessage);
        }

        [TestMethod]
        public void Text_IsRequired()
        {
            // Arrange: Create a Note instance without text
            var note = new Note { Title = "Note Title" };

            // Act: Attempt to validate the Note instance
            var context = new ValidationContext(note, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, context, result, true);

            // Assert: Verify that the validation fails and reports the required text
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("You must fill in the text", result[0].ErrorMessage);
        }
    }
}
