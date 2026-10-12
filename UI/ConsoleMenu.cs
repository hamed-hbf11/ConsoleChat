using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleChat.Interfaces;

namespace ConsoleChat.UI
{
    public class ConsoleMenu
    {
        private readonly IAuthService? _authService;
        private readonly IUserService? _userService;

        public ConsoleMenu(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public void RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== CONSOLE CHAT ==========\n");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandlerRegister();
                        break;
                    case "2":
                        HandleLogin();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void HandleLogin()
        {
            Console.Clear();
            Console.WriteLine("========== LOGIN ==========\n");

            Console.Write("Username: ");
            string username = Console.ReadLine() ?? string.Empty;

            Console.Write("Password: ");
            string password = ReadPassword();

            Console.WriteLine();
            bool success = _authService?.Login(username, password) ?? false;

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            if (success)
            {
                RunUserMenu();
            }
        }

        private void RunUserMenu()
        {
            while (true)
            {
                var currentUser = _authService?.GetCurrentUser();
                if (currentUser == null)
                    break;

                Console.Clear();
                Console.WriteLine($"========== USER MENU ==========\n");
                Console.WriteLine($"Welcome, {currentUser.Username}!");
                Console.WriteLine("1. View Users");
                Console.WriteLine("2. Send Message");
                Console.WriteLine("3. Inbox");
                Console.WriteLine("4. Conversation History");
                Console.WriteLine("0. Logout\n");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleViewUsers();
                        break;
                    case "0":
                        _authService?.Logout();
                        Console.WriteLine("\nLogged out successfully. Press any key...");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("\nInvalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void HandleViewUsers()
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("========== USERS ==========\n");
            var currentUser = _authService?.GetCurrentUser();
            var allUsers = _userService?.GetAllUsers();

            var otherUsers = allUsers?.Where(u => u.UserId != currentUser?.UserId).ToList();

            if (!otherUsers?.Any() ?? true)
            {
                Console.WriteLine("No other users registered yet.");
            }
            else
            {
                for (int i = 0; i < otherUsers?.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {otherUsers[i].Username}");
                }
            }

            Console.WriteLine("\n0. Back");
            Console.Write("\nChoose: ");
            Console.ReadLine();
        }

        private void HandlerRegister()
        {
            Console.Clear();
            Console.WriteLine("========== REGISTER ==========\n");

            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = ReadPassword();

            Console.WriteLine();
            _authService?.Register(username, password);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        private string ReadPassword()
        {
            string password = "";
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return password;
        }
    }
}