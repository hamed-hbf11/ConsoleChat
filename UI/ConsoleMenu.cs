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

        public ConsoleMenu(IAuthService authService)
        {
            _authService = authService;
        }

        public void RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== CONSOLE CHAT ==========\n");
                Console.WriteLine("1. Register");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandlerRegister();
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