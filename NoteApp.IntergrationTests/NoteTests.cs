using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.IntergrationTests
{
    [TestFixture]
    public class NoteTests
    {
        [Test]
        public void Title_RequiredAttribute_Should_Validate()
        {
            // Arrange: Create a new Note instance without setting the required Title
            var note = new Note();

            // Act: Attempt to validate the object using Data Annotations validation
            var validationContext = new ValidationContext(note);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, validationContext, validationResults, true);

            // Assert: Verify that the validation fails and contains an error for Title
            Assert.IsFalse(isValid, "The Title should be required");
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("Title")), "Validation result should contain an error for Title");
        }

        [Test]
        public void Title_StringLengthAttribute_Should_Validate()
        {
            // Arrange: Create a new Note instance with an empty Title
            var note = new Note
            {
                Title = ""
            };

            // Act: Attempt to validate the object using Data Annotations validation
            var validationContext = new ValidationContext(note);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, validationContext, validationResults, true);

            // Assert: Verify that the validation fails due to Title length constraint
            Assert.IsFalse(isValid, "The Title length should be between 1 and 100 characters");
        }

        [Test]
        public void Text_RequiredAttribute_Should_Validate()
        {
            // Arrange: Create a new Note instance without setting the required Text
            var note = new Note();

            // Act: Attempt to validate the object using Data Annotations validation
            var validationContext = new ValidationContext(note);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(note, validationContext, validationResults, true);

            // Assert: Verify that the validation fails and contains an error for Text
            Assert.IsFalse(isValid, "The Text should be required");
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("Text")), "Validation result should contain an error for Text");
        }

        [Test]
        public void CreatedAt_DefaultValue_Should_Be_Now()
        {
            // Arrange: Create a new Note instance
            var note = new Note();

            // Act: Get the current time
            DateTime currentTime = DateTime.Now;

            // Assert: Verify that the default value of CreatedAt is close to the current time
            Assert.That(note.CreatedAt, Is.EqualTo(currentTime).Within(TimeSpan.FromSeconds(1)));
        }
    }
}
