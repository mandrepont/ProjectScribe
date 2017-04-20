using System;
using System.Collections.Generic;
using System.Text;

namespace ScribeClient.Commands
{
    public interface ICommand
    {
        string Command { get;}
        string Description { get;}
        string Ussage { get;}
        void  Execute(string[] args);
    }
}
