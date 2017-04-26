using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScribeServer.Models;
using ScribeServer.Data;
using Microsoft.AspNetCore.Authorization;

namespace ScribeServer.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        /// <summary>
        /// ROUTE: api/notes
        /// Gets a list of all the notes in the current general repository.
        /// WARNING: If the note program has large amounts of notes this could be a bottle neck.
        /// Instead a most recent created list would be a better idea in a large environment
        /// Also server side search would have to be implemented. 
        /// </summary>
        /// <returns>JSON Array of all notes.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Note> Get()
        {
            return Database.GlobalDatabase.Notes.Where(n => !n.IsPrivate);
        }

        /// <summary>
        /// ROUTE: api/notes/private
        /// Gets a list of all the private notes made by a user.
        /// WARNING: If the note program has large amounts of notes this could be a bottle neck.
        /// Instead a most recent created list would be a better idea in a large environment
        /// Also server side search would have to be implemented. 
        /// </summary>
        /// <returns>JSON Array of all private notes.</returns>
        [HttpGet("private")]
        [Authorize]
        public IEnumerable<Note> GetPrivate()
        {
            var sub = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            var user = Database.GlobalDatabase.Users.FirstOrDefault(u => u.AuthorId == sub);
            if(user.Role != UserRoles.Admin)
                return Database.GlobalDatabase.Notes.Where(n => n.AuthorId == user.AuthorId && n.IsPrivate);
            else
                return Database.GlobalDatabase.Notes.Where(n => n.IsPrivate);
        }

        /// <summary>
        /// ROUTE: api/notes/private/{id}
        /// Used for getting logged in members private content.
        /// NOTE: id:int means that it takes a strict data type of int
        /// EX: GET: api/notes/abd will return 404.
        /// </summary>
        /// <param name="id">ID of the note.</param>
        /// <returns>Returns 204 if no note is found, and 200 if the note is found with the note as the body.</returns>
        [HttpGet("private/{id:int}")]
        [Authorize]
        public Note GetPrivate(int id)
        {
            var sub = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            var user = Database.GlobalDatabase.Users.FirstOrDefault(u => u.AuthorId == sub);
            if(user.Role != UserRoles.Admin)
                return Database.GlobalDatabase.Notes.FirstOrDefault(n => n.AuthorId == user.AuthorId && n.IsPrivate && n.Id == id);
            else
                return Database.GlobalDatabase.Notes.FirstOrDefault(n => n.IsPrivate && n.Id == id);
        }

        /// <summary>
        /// ROUTE: GET: api/notes/id
        /// Gets the note associated with the id in the URI.
        /// NOTE: id:int means that it takes a strict data type of int
        /// EX: GET: api/notes/abd will return 404.
        /// </summary>
        /// <param name="id">Id of the note.</param>
        /// <returns>Returns 204 if no note is found, and 200 if the note is found with the note as the body.</returns>
        [HttpGet("{id:int}")]
        public Note Get(int id)
        {
            return  Database.GlobalDatabase.Notes.SingleOrDefault(s=> s.Id == id && !s.IsPrivate);
        }

        /// <summary>
        /// ROUTE: POST api/notes/
        /// Used for adding new notes to the general repository
        /// TODO:Add private repo for users.
        /// </summary>
        /// <param name="content">Note requesting to be added.</param>
        /// <returns>
        /// Returns the HTTP standard status codes for created content on a post.
        /// 401 for unauthorized
        /// 400 for bad format.
        /// 500 for internal error adding the database.
        /// </returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Note content)
        {
            var sub = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            var user = Database.GlobalDatabase.Users.FirstOrDefault(u => u.AuthorId == sub);
            //MVC Auto serialized the json into the object, however if the content is null
            //then there was a problem with the serializing of the object
            //Because our model has required field we need to make sure the client respected
            // those conditions.
            if (content!=null && ModelState.IsValid)
            {
                content.AuthorId = user.AuthorId;
                content.AuthorName = user.Name;
                if (NoteHandler.AddNote(content))
                    return StatusCode(201);
                else
                    return StatusCode(500);               
            }
            
            //Bad Request format.
            return StatusCode(400);
        }

        /// <summary>
        /// ROUTE: PUT /api/notes/id
        /// Updated for updating database information on notes.
        /// Normally a put can also act as a post, but I do not want it 
        /// to in this application.
        /// NOTE: id:int means that it takes a strict datatype of int
        /// EX: PUT: api/notes/abd will return 404.
        /// <returns>
        /// Returns the HTTP standard status codes for created content on a put.
        /// 400 for bad format.
        /// 500 for internal error adding the database.
        /// </returns>
        [HttpPut("{id:int}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody]Note content)
        {
            var sub = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            var user = Database.GlobalDatabase.Users.FirstOrDefault(u => u.AuthorId == sub);
            if (content != null)
            {
                if (content.AuthorId == user.AuthorId || user.Role == UserRoles.Admin)
                {
                    if (NoteHandler.UpdateNote(content))
                        return new CreatedAtActionResult("notes", "GET", id, content);
                    else
                        return StatusCode(500);
                }
                else return Unauthorized();
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
        /// <returns>HTTP standard of 204 for created and 400 for invalid request. 401 for unauthorized permissions.</returns>
        [HttpDelete("{id:int}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var sub = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            var user = Database.GlobalDatabase.Users.FirstOrDefault(u => u.AuthorId == sub);
            bool removed;
            if(user.Role == UserRoles.Admin)
                removed = NoteHandler.RemoveNote(id);
            else if (Database.GlobalDatabase.Notes.FirstOrDefault(n => n.AuthorId == user.AuthorId && n.Id == id) != null)
                removed = NoteHandler.RemoveNote(id);
            else
                return Unauthorized();
            if (removed)
                return StatusCode(204);
            else
                return StatusCode(400);
        }
    }
}
