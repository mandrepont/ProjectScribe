using System;
using System.Collections.Generic;
using System.Linq;
using ScribeServer.Data;
using ScribeServer.Models;
using Xunit;

namespace ScribeTest.ServerTest
{
    ///// <summary>
    ///// While the name might be missleading, the database is also tested via this test. 
    ///// </summary>
    //public class NoteHanlerTest
    //{
    //    /// <summary>
    //    /// Called before any test so that we can create the database.
    //    /// </summary>
    //    public NoteHanlerTest()
    //    {
    //        Database.CheckDatabase();
    //    }

    //    /// <summary>
    //    /// Adds a new note to the database.
    //    /// </summary>
    //    [Fact]
    //    public void AddTest()
    //    {
    //        var note = new Note()
    //        {
    //            AuthorName = "Michael",
    //            Title = "Testing out a title",
    //            Content = "Test filler content"
    //        };
    //        NoteHandler.AddNote(note);
    //        var retrievedNote = Database.GlobalDatabase.Notes.FirstOrDefault(s => s.Id == Database.GlobalDatabase.LastNoteID);
    //        Assert.NotNull(retrievedNote);
    //        Assert.Equal(note.AuthorName, retrievedNote.AuthorName);
    //        Assert.Equal(note.Content, retrievedNote.Content);
    //        Assert.Equal(note.Title, retrievedNote.Title);
    //    }

    //    /// <summary>
    //    /// Add note must be ran before this test.
    //    /// Check if the note being updated really is updated.
    //    /// </summary>
    //    [Fact]
    //    public void UpdateTest()
    //    {
    //        var retrievedNote = Database.GlobalDatabase.Notes.FirstOrDefault(s => s.Id == Database.GlobalDatabase.LastNoteID);
    //        var newNote = retrievedNote;
    //        newNote.Title = "Yep edited note.";
    //        Assert.False(!NoteHandler.UpdateNote(newNote));
    //    }
    //    /// <summary>
    //    /// Only works if the add test was ran before this.
    //    /// Removes the added note and check if it was really removed. 
    //    /// </summary>
    //    [Fact]
    //    public void RemoveTest()
    //    {
    //        var retrievedNote = Database.GlobalDatabase.Notes.FirstOrDefault(s => s.Id == Database.GlobalDatabase.LastNoteID);
    //        Assert.False(!NoteHandler.RemoveNote(retrievedNote.Id));
    //        Assert.NotNull(retrievedNote);
    //        Assert.Null(Database.GlobalDatabase.Notes.FirstOrDefault(s => s.Id == Database.GlobalDatabase.LastNoteID));

    //    }

    //}
}
