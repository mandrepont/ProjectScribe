using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScribeServer.Models;
using ScribeServer.Data;

namespace ScribeServer.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        /// <summary>
        /// ROUTE: api/notes
        /// Gets a list of all the notes in the current general repository.
        /// WARNING: If the note program has large amounts of notes this could be a bottle neck.
        /// Instead a most recent created list would be a better idea in a large enviroment
        /// Also server side search would have to be implimented. 
        /// </summary>
        /// <returns>JSON Array of all notes.</returns>
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return Database.GlobalDatabase.Notes;
        }

        /// <summary>
        /// ROUTE: GET: api/notes/id
        /// Gets the note assosated with the id in the URI.
        /// NOTE: id:int means that it takes a strict datatype of int
        /// EX: GET: api/notes/abd will return 404.
        /// </summary>
        /// <param name="id">Id of the note.</param>
        /// <returns>Returns 204 if no note is found, and 200 if the note is found with the note as the body.</returns>
        [HttpGet("{id:int}")]
        public Note Get(int id)
        {
            return  Database.GlobalDatabase.Notes.SingleOrDefault(s=> s.Id == id);
        }

        /// <summary>
        /// ROUTE: POST api/notes/
        /// Used for adding new notes to the general respository
        /// TODO:Add private repo for users.
        /// </summary>
        /// <param name="content">Note requesting to be added.</param>
        /// <returns>
        /// Returns the HTTP standard status codes for created content on a post.
        /// 400 for bad format.
        /// 500 for internal error adding the database.
        /// </returns>
        [HttpPost]
        public IActionResult Post([FromBody]Note content)
        {
            //MVC Auto searlized the json into the object, however if the content is null
            //then there was a problem with the searization of the object
            //Becuase our model has required field we need to make sure the client respected
            // those conditions.
            if(content!=null && ModelState.IsValid)
            {

                if (NoteHandler.AddNote(content))
                    return new CreatedAtActionResult("notes", "GET", Database.GlobalDatabase.LastNoteID, content);
                else
                    return StatusCode(500);               
            }
            
            //Bad Request format.
            return StatusCode(400);
        }

        /// <summary>
        /// ROUTE: PUT /api/notes/id
        /// Updated for updating database information on notes.
        /// Noramally a put can also act as a post, but I do not want it 
        /// to in this application.
        /// NOTE: id:int means that it takes a strict datatype of int
        /// EX: PUT: api/notes/abd will return 404.
        /// <returns>
        /// Returns the HTTP standard status codes for created content on a put.
        /// 400 for bad format.
        /// 500 for internal error adding the database.
        /// </returns>
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody]Note content)
        {
            if (content != null)
            {
                if (NoteHandler.UpdateNote(content))
                    return new CreatedAtActionResult("notes", "GET", id, content);
                else
                    return StatusCode(500);
            }

            //Bad Request format.
            return StatusCode(400);
        }

        /// <summary>
        /// ROUTE: DELETE /api/notes/id
        /// Removes a note from the global repository.
        /// TODO: Check permissions.
        /// NOTE: id:int means that it takes a strict datatype of int
        /// EX: DELETE: api/notes/abd will return 404.
        /// </summary>
        /// <param name="id">Id of the note being deleted</param>
        /// <returns>HTTP standard of 204 for created and 400 for invalid request.</returns>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var removed = NoteHandler.RemoveNote(id);
            if (removed)
                return StatusCode(204);
            else
                return StatusCode(400);
        }
    }
}
