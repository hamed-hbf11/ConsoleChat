using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ConsoleChat.Interfaces;
using ConsoleChat.Models;
using System.Text;

namespace ConsoleChat.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User>? _users;
        private User? _currentUser;
        public AuthService(List<User> users)
        {
            _users = users;
        }
        public User? GetCurrentUser() => _currentUser;

        public bool Login(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and Password cannot be empty.");
                return false;
            }
            var user = _users?.FirstOrDefault(u=> u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if(user == null)
            {
                Console.WriteLine("Invalid username or password.");
                return false;
            }
            string hashedPassword = HashPassword(password, user.PasswordSalt);
            if(user.PasswordHash != hashedPassword)
            {
                Console.WriteLine("Invalid username or password.");
                return false;
            }
            _currentUser = user;
            _currentUser.IsOnline = true;
            _currentUser.LastSeen = DateTime.Now;

            Console.WriteLine($"Welcome back, {user.Username}!");
            return true;
        }

        public void Logout()
        {
            if(_currentUser != null)
            {
                _currentUser.IsOnline = false;
                _currentUser.LastSeen = DateTime.Now;
                _currentUser = null;
            }
        }

        public bool Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and Password cannot be empty.");
                return false;
            }

            if (_users?.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) == true)
            {
                Console.WriteLine("Username already exists. Please choose another one.");
                return false;
            }

            string salt = GenerateSalt();
            string hash = HashPassword(password, salt);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsOnline = false,
                LastSeen = DateTime.Now
            };

            _users?.Add(newUser);
            Console.WriteLine("Registration successful!");
            return true;
        }

        #region Helper Methods For Password Hashing
        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        private static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        #endregion
    }
}