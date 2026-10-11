using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleChat.Interfaces
{
    public interface IAuthService
    {
        bool Register(string username, string password);
        bool Login(string username, string password);
        void Logout();
        Models.User? GetCurrentUser();
    }
}