using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Commands
{
    class LookUp : ICommand
    {
        public string Command => "lookup";

        public string Description => "Searches the public list and if user is logged in also the private list";

        public string Usage => "lookup [title key word or words]";

        /// <summary>
        /// Used for Searching public list.
        /// Might add an option such as lookup -p [term] to search private
        /// </summary>
        /// <param name="args">Command args.</param>
        public void Execute(string[] args)
        {
            var commandString = string.Join(" ", args);
            var term = commandString.Remove(0, 7);
            var result = Task.Run(() => Service.NoteService.GetPublicList());
            //TODO:Get private list as well
            if (result.Result != null)
            {
                List<Note> foundNotes = new List<Note>();
                foreach (var note in result.Result)
                    if (note.Title.ToUpper().Contains(term.ToUpper()))
                        foundNotes.Add(note);

                foreach (var note in foundNotes)
                {
                    Console.WriteLine("Id: " + note.Id + " Title: " + note.Title);
                }
            }
        }
    }
}
