using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleChat.Models;

namespace ConsoleChat.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User? GetUserById(string id);
        User? GetUserByUsername(string username);
        void UpdateUserStatus(string userId, bool isOnline);
    }
}