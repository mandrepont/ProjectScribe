using System;
using System.Collections.Generic;
using System.Text;

namespace ScribeClient.Commands
{
    /// <summary>
    /// Command interface used for every command. 
    /// </summary>
    public interface ICommand
    {
        string Command { get;}
        string Description { get;}
        string Ussage { get;}
        void  Execute(string[] args);
    }
}
