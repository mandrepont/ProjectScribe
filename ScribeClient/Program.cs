using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net;

namespace ScribeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmdHandler = new CommandHandler();
            Console.WriteLine("Now listening for commands, use help for a list of commands.");
            //Endless loop of commands.
            while (true)
            {
                cmdHandler.Listen();
            }
        }
    }
}