using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ScribeClient.Commands;
using System.Reflection;

namespace ScribeClient
{
    public class CommandHandler
    {
        private List<ICommand> Commands;

        /// <summary>
        /// Register all command handlers.
        /// </summary>
        public CommandHandler()
        {
            Commands = new List<ICommand>();
            //Sadly in dotnet.core getting executing assembly is not yet implimented. 
            Commands.Add(new CreateNote());
            Commands.Add(new ListNote());
            Commands.Add(new Clear());
            Commands.Add(new Exit());
            Commands.Add(new LookUp());
            Commands.Add(new RemoveNote());
            Commands.Add(new EditNote());
            Commands.Add(new Login());
        }

        /// <summary>
        /// Listen for the next incoming command.
        /// </summary>
        public void Listen()
        {
            //Draw command prompt
            Console.Write(">: ");
            var command = Console.ReadLine();
            var args = command.Split(' ');
            HandleCommand(args);
        }

        /// <summary>
        /// Determin how to handle a command what what should be done.
        /// Called by the listener.
        /// </summary>
        /// <param name="command">Command imput.</param>
        private void HandleCommand(string[] command)
        {
            if (command[0] == "help")
            {
                Console.WriteLine(string.Format("{0,-12} || {1}", "[ActionWord]", "[Description]"));
                foreach (var cmd in Commands)
                    Console.WriteLine(string.Format("{0,-12} || {1}", cmd.Command, cmd.Description));
                Console.WriteLine("Put -h as the first argument on any command for ussage information.");
            }
            else
            {
                var cmd = Commands.FirstOrDefault(c => c.Command == command[0]);
                if (cmd != null)
                {
                    if (command.Length > 1)
                    {
                        //Check if the user just needs help rather than executing
                        if (command[1] == "-h" || command[1] == "-help")
                            Console.WriteLine(cmd.Ussage);
                        else cmd.Execute(command);
                    }
                    else cmd.Execute(command);

                }
                else
                    Console.WriteLine("Command not found, type help for a list of commands.");
            }

        }
    }
}
