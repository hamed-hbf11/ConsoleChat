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
            var messages = new List<Message>();

            var authservice = new AuthService(users);
            var userService = new UserService(users);
            var messageService = new MessageService(messages);
            
            var menu = new ConsoleMenu(authservice, userService, messageService);

            menu.RunMainMenu();
        }
    }
}