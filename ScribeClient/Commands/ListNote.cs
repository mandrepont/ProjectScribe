using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Commands
{
    public class ListNote : ICommand
    {
        public string Command => "lsnote";

        public string Description => "List notes from public, private respositories. Or a single note by its ID";

        public string Ussage => "lsnote [Options] \n Options: \n public : returns all public note's title and ids\n private : Returns all private notes posted by the current logged in user" +
            "\n noteid : Returns a particular note in detail EX 'lsnote 3'\n private {id} : Returns details about a private note created by logged in user.";

        /// <summary>
        /// This commands determins what list or indivdual note the user is request and gets that list or note. 
        /// </summary>
        /// <param name="args"></param>
        public void Execute(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine("Invalid Command Ussage type lsnote -h for help");
                return;
            }
            if (args[1] == "public")
            {
                var result = Task.Run(() => Service.NoteService.GetPublicList());
                if (result.Result != null)
                {
                    foreach (var note in result.Result)
                    {
                        Console.WriteLine("Id: " + note.Id + " Title: " + note.Title);
                    }
                }
            }
            else if (args[1] == "private")
            {
                
                if(Constants.AccessToken == null)
                {
                    Console.WriteLine("User must be logged in to use this command. Login with 'login' command.");
                    return;
                }
                //User is requesting a private note by id.
                if (args.Length == 3)
                {
                    if (int.TryParse(args[2], out int id))
                    {
                        var task = Task.Run(() => Service.NoteService.GetPrivateNoteById(id));
                        if (task.Result != null)
                        {
                            Console.WriteLine("Title: " + task.Result.Title);
                            Console.WriteLine("Author: " + task.Result.AuthorName);
                            Console.WriteLine("Orginally Posted: " + task.Result.CreationDate + "|| Last Updated: " + task.Result.LastUpdate);
                            Console.WriteLine("Content: " + task.Result.Content);
                        }
                        else
                            Console.WriteLine("Note with ID of " + id + " not found.");
                        return;
                    }
                    else
                        Console.WriteLine("Invalid Command Ussage type lsnote -h for help");
                }
                var result = Task.Run(() => Service.NoteService.GetPrivateList());
                if (result.Result != null)
                {
                    foreach (var note in result.Result)
                    {
                        Console.WriteLine("Id: " + note.Id + " Title: " + note.Title);
                    }
                }
            }
            else if (int.TryParse(args[1], out int id))
            {
                var task = Task.Run(() => Service.NoteService.GetNoteById(id));
                if (task.Result != null)
                {
                    Console.WriteLine("Title: " + task.Result.Title);
                    Console.WriteLine("Author: " + task.Result.AuthorName);
                    Console.WriteLine("Orginally Posted: " + task.Result.CreationDate + "|| Last Updated: " + task.Result.LastUpdate);
                    Console.WriteLine("Content: " + task.Result.Content);
                }
                else
                    Console.WriteLine("Note with ID of " + id + " not found.");
                return;
            }
            else
                Console.WriteLine("Invalid Command Ussage type lsnote -h for help");
        }
    }
}
