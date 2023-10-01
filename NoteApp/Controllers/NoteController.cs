using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Interfaces;
using NoteApp.Models;
using System.Collections.Generic;

namespace NoteApp.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INote _INote;

        public NoteController(INote iNote)
        {
            _INote = iNote;
        }

        // HTTP GET method for retrieving a list of all notes.
        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
            return await Task.FromResult(_INote.GetNotes());
        }

        // HTTP GET method for retrieving a note by its identifier.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Note user = _INote.GetNoteById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // HTTP POST method for adding a new note.
        [HttpPost]
        public void Post(Note note)
        {
            _INote.AddNote(note);
        }

        // HTTP PUT method for updating an existing note.
        [HttpPut]
        public void Put(Note note)
        {
            _INote.UpdateNote(note);
        }
    }
}
