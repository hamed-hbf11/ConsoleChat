using ConsoleChat.Models;
using ConsoleChat.Services;
using ConsoleChat.UI;

namespace ConsoleChat
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var users = new List<User>();
            var authservice = new AuthService(users);
            var menu = new ConsoleMenu(authservice);

            menu.RunMainMenu();
        }
    }
}