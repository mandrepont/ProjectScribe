﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Commands
{
    public class Login : ICommand
    {
        public string Command => "login";

        public string Description => "Allows a user to login.";

        public string Usage => "login [username] [password]";

        /// <summary>
        /// Executes a login authentication with the auth server.
        /// </summary>
        /// <param name="args">Command args.</param>
        public void Execute(string[] args)
        {
            if(args.Length == 3)
            {
                var task = Task.Run(() => Service.AccountService.Authenticate(args[1], args[2]));
                if (task.Result)
                {
                    Console.WriteLine("Account Authenticated");
                    var infoTask = Task.Run(() => Service.AccountService.GetValues());
                    Console.WriteLine(infoTask.Result);
                }
            }
            else
                Console.WriteLine("Invalid Command Usage type login -h for help");

            
        }
    }
}
