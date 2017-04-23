using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Commands
{
    class CreateNote : ICommand
    {
        public string Command { get => "mknote"; }
        public string Description { get => "Creats a new note using either anonymous or currently logged in client."; }
        public string Usage { get => "Prompt"; }

        /// <summary>
        /// Checks if the user is logged in and then promps the user on creating a new note.
        /// </summary>
        /// <param name="args">Command args.</param>
        public void Execute(string[] args)
        {
            //TODO: If the user is note logged in use the anonymous note channel. 
            if (Constants.AccessToken == null)
            {
                Console.WriteLine("User must be logged in to use this command. Login with 'login' command.");
                return;
            }
            
            var note = new Note();
            Console.WriteLine("Please enter the title of the note: (Enter to Advance)");
            note.Title = Console.ReadLine();
            Console.WriteLine("Enter the content of the note: (Enter to Advance)");
            note.Content = Console.ReadLine();
            Console.WriteLine("Press 'y' to make the note private, any other button to make the note public.");
            note.IsPrivate = Console.ReadKey().Key.Equals(ConsoleKey.Y);
            var result = Task.Run(() => Service.NoteService.PostGeneralNote(note));
            if (result.Result)
                Console.WriteLine("\nNote Created");
            else
                Console.WriteLine("\nNote Was Not Created");
        }
    }
}
