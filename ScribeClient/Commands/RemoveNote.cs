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

        public string Ussage => "type rmnote {id} where id is the note id you wish to remove.";

        public void Execute(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Invalid Command Ussage type editnote -h for help");
                return;
            }

            if (int.TryParse(args[1], out int id))
            {
                var result = Task.Run(() => NoteService.RemoveNote(id));
                if (result.Result)
                    Console.WriteLine("Note removed");
                else
                    Console.WriteLine("The note was note removed, perhaps invalid permission or invalid id.");
            }
            else
                Console.WriteLine("Invalid command ussage, type rmnote -h for ussage information.");
        }
    }
}
