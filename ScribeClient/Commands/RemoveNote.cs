using ScribeClient.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Commands
{
    public class RemoveNote : ICommand
    {
        public string Command => "rmnote";

        public string Description => "Removes a note in which you are the author of or have permission to.";

        public string Usage => "type rmnote {id} where id is the note id you wish to remove.";

        /// <summary>
        /// Removes a note from the server.
        /// </summary>
        /// <param name="args">Command args.</param>
        public void Execute(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Invalid Command Usage type rmnote -h for help");
                return;
            }
            if (Constants.AccessToken == null)
            {
                Console.WriteLine("User must be logged in to use this command. Login with 'login' command.");
                return;
            }
            //TODO: Check if the user is logged in.
            if (int.TryParse(args[1], out int id))
            {
                var result = Task.Run(() => NoteService.RemoveNote(id));
                if (result.Result)
                    Console.WriteLine("Note removed");
                else
                    Console.WriteLine("The note was note removed, perhaps invalid permission or invalid id.");
            }
            else
                Console.WriteLine("Invalid command Usage, type rmnote -h for Usage information.");
        }
    }
}
