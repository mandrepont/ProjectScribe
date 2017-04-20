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
        public string Ussage { get => "Prompt"; }

        public void Execute(string[] args)
        {
            var note = new Note();
            Console.WriteLine("Please enter the title of the note: (Enter to Advance)");
            note.Title = Console.ReadLine();
            Console.WriteLine("Enter the cotent of the note: (Enter to Advance)");
            note.Content = Console.ReadLine();
            //TODO: Promp Private/public
            note.AuthorName = "Panda";
            note.Id = 0;
            var result = Task.Run(() => Service.NoteService.PostGeneralNote(note));
            if (result.Result)
                Console.WriteLine("Note Created");
            else
                Console.WriteLine("Note Was Not Created");
        }
    }
}
