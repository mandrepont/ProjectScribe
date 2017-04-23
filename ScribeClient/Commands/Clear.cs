using System;
using System.Collections.Generic;
using System.Text;

namespace ScribeClient.Commands
{
    public class Clear : ICommand
    {
        public string Command => "clear";

        public string Description => "Clears the console window";

        public string Ussage => "Just type clear and watch the magic happen...";

        /// <summary>
        /// Simply clears the console.
        /// </summary>
        /// <param name="args">Command args.</param>
        public void Execute(string[] args)
        {
            Console.Clear();
        }
    }
}
