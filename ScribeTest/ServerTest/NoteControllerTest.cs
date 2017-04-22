using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ScribeServer.Controllers;
using ScribeServer.Models;

namespace ScribeTest.ServerTest
{
    /// <summary>
    /// Class meant to test the controller of the note
    /// not the routing.
    /// </summary>
    public class NoteControllerTest
    {
        private IList<Note> AddedNotes;
        private NotesController controller;

        /// <summary>
        /// For this constructer we do not get live data so we must make the database in a mock way.
        /// </summary>
        public NoteControllerTest()
        {
            controller = new NotesController();
            AddedNotes = new List<Note>();
            int numOfNotes = 3;
            Database.GlobalDatabase = new Database();
            Database.GlobalDatabase.Notes = new List<Note>();
            for (int i = 0; i <= numOfNotes; i++)
            {
                var note = new Note()
                {
                    Id = i,
                    AuthorId = i,
                    Title = "Note Num: " + i,
                    Content = "Content test Num" + i,
                    AuthorName = "Name " + i
                };
                
                Database.GlobalDatabase.Notes.Add(note);
                AddedNotes.Add(note);
            }
        }
        /// <summary>
        /// Gets all of the current notes int the mock DB
        /// Ensures valid response.
        /// </summary>
        [Fact]
        public void GetAllTest()
        {
            var gotNotes = controller.Get();
            Assert.Equal(AddedNotes, gotNotes);
        }

        /// <summary>
        /// Test if the notes are really got out of the database.
        /// Checks a few with one that does not exist.
        /// </summary>
        /// <param name="i">id of note.</param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetIdTest(int i)
        {
            var note = controller.Get(i);
            if (i != 3)
                Assert.Equal(AddedNotes[i], note);
            else
                Assert.NotNull(note);
        }


        /// <summary>
        /// Edits the 3rd note and gets it out the database.
        /// </summary>
        [Fact]
        public void PutTest()
        {
            AddedNotes[2].AuthorName = "UpdatedName";
            controller.Put(2, AddedNotes[2]);
            Assert.NotNull(Database.GlobalDatabase.Notes.First(n => n.AuthorName == "UpdatedName"));
        }
        /// <summary>
        /// Deletes one of the added notes And then test to see if it is really deleted.
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            controller.Delete(AddedNotes[1].Id);
            Assert.False(Database.GlobalDatabase.Notes.Contains(AddedNotes[1]));
        }
    }
}
