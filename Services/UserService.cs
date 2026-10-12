using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleChat.Interfaces;
using ConsoleChat.Models;

namespace ConsoleChat.Services
{
    public class UserService : IUserService
    {
        private readonly List<User>? _users;
        public UserService(List<User> users)
        {
            _users = users;
        }
        public List<User> GetAllUsers() => _users;

        public User? GetUserById(string id)
        {
            return _users?.FirstOrDefault(u => u.UserId == id);
        }

        public User? GetUserByUsername(string username)
        {
            return _users?.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateUserStatus(string userId, bool isOnline)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.IsOnline = isOnline;
                user.LastSeen = DateTime.Now;
            }
        }
    }
}