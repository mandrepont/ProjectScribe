using ScribeServer.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScribeServer.Data
{
    /// <summary>
    /// NoteHandler class is designed to be a helper to the data layer accessed from the web api.
    /// Functions should include; Update, Add, and Remove. 
    /// </summary>
    public class NoteHandler
    {
        /// <summary>
        /// Updates the note with critical information about the note.
        /// Adds the note to the current static database.
        /// Writes the static database over to file system.
        /// </summary>
        /// <param name="note">Note being Added.</param>
        /// <returns>True of note was added, false if note was note added.</returns>
        public static bool AddNote(Note note)
        {
            try
            {
                Database.GlobalDatabase.LastNoteID += 1;
                note.CreationDate = DateTime.Now;
                note.LastUpdate = DateTime.Now;
                note.Id = Database.GlobalDatabase.LastNoteID;
                Database.GlobalDatabase.Notes.Add(note);
                Database.UpdateDB();
                return true;
            }
            catch (Exception e)
            {
                //Only show excpetion message in debug env.
#if DEBUG
                Console.WriteLine(e);
#endif
                return false;
            }
        }

        /// <summary>
        /// Selects the note from the database and removes it from the static database.
        /// Writes static database to filesystem.
        /// </summary>
        /// <param name="id">Int of note ID</param>
        /// <returns>True of note was removed, false if note was note removed.</returns>
        public static bool RemoveNote(int id)
        {
            try
            {
                var note = Database.GlobalDatabase.Notes.FirstOrDefault(s => s.Id == id);
                //note not found.
                if (note == null)
                    return false;
                Database.GlobalDatabase.Notes.Remove(note);
                Database.UpdateDB();
                return true;
            }
            catch (Exception e)
            {
                //Only show excpetion message in debug env.
#if DEBUG
                Console.WriteLine(e);
#endif
                return false;
            }
        }

        /// <summary>
        /// Beucase of the list there is no key value to update, so we must first 
        /// remove the old note and add a new note with the updated information.
        /// </summary>
        /// <param name="note">Updated Note.</param>
        /// <returns>True of note was updated, false if note was note updated.</returns>
        public static bool UpdateNote(Note note)
        {
            try
            {
                var oldNote = Database.GlobalDatabase.Notes.FirstOrDefault(s => s.Id == note.Id);
                //note not found.
                if (note == null)
                    return false;
                //No real update function on a list, so out with the old in with the new.
                note.LastUpdate = DateTime.Now;
                Database.GlobalDatabase.Notes.Remove(oldNote);
                Database.GlobalDatabase.Notes.Add(note);
                Database.UpdateDB();
                return true;
            }
            catch (Exception e)
            {
                //Only show excpetion message in debug env.
#if DEBUG
                Console.WriteLine(e);
#endif
                return false;
            }
        }
    }
}
