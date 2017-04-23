using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Commands
{
    public class EditNote : ICommand
    {
        public string Command => "editnote";

        public string Description => "Edits and existing note that logged in user is able to edit.";

        public string Ussage => "editnote {id}, where id is the note id you wish to edit then follow the prompts";

        /// <summary>
        /// Edit note is not how I would like it to be, but what can I expect for a console application.
        /// simply allows users to edit their post.
        /// </summary>
        /// <param name="args">Command args.</param>
        public void Execute(string[] args)
        {
            if(args.Length <= 1)
            {
                Console.WriteLine("Invalid Command Ussage type editnote -h for help");
                return;
            }
            if (int.TryParse(args[1], out int id))
            {
                var task = Task.Run(() => Service.NoteService.GetNoteById(id));
                if (task.Result != null)
                {
                    var note = task.Result;
                    bool change = false;

                    Console.WriteLine("DISCLAIMER: Due to dot net core I am not able to simulate keystrokes for the purpose editable lines\n" +
                "with that said I will present the old title and the next line will be the input for the new title. Same with content. \n" +
                "Leave input blank to leave the field the same.");

                    Console.WriteLine("Title: " + task.Result.Title);
                    var newTitle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        note.Title = newTitle;
                        change = true;
                    }
                        
                    Console.WriteLine("Content: " + task.Result.Content);
                    var newContent = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newContent))
                    {
                        note.Content = newContent;
                        change = true;
                    }
                    //We do not want to put if there is no change.
                    if (change)
                    {
                        var upTask = Task.Run(() => Service.NoteService.UpdateNote(note));
                        if (upTask.Result)
                            Console.WriteLine("The note was successfully updated.");
                        else
                            Console.WriteLine("The note was not able to be updated.");
                    }
                    else
                        Console.WriteLine("No change in note, no update need.");
                        
                }
                else
                    Console.WriteLine("Note with ID of " + id + " not found.");
                return;
            }
            else
                Console.WriteLine("Invalid Command Ussage type editnote -h for help");
            ;
        }
    }
}
