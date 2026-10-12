using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleChat.Interfaces;
using ConsoleChat.Models;

namespace ConsoleChat.Services
{
    public class MessageService : IMessageService
    {
        private readonly List<Message> _messages;
        public MessageService(List<Message> messages)
        {
            _messages = messages;
        }

        public List<Message> GetConversation(string userId, string user2Id)
        {
            return _messages
            .Where(m => (m.SenderId == userId && m.ReceiverId == user2Id)
                    || (m.SenderId == user2Id && m.ReceiverId == userId))
            .OrderBy(m => m.Timestamp)
            .ToList();
        }

        public List<Message> GetInbox(string userId)
        {
            return _messages
            .Where(m => m.ReceiverId == userId)
            .OrderByDescending(m => m.Timestamp)
            .ToList();
        }

        public int GetUnreadCount(string userId)
        {
            return _messages.Count(m => m.ReceiverId == userId && !m.IsRead);
        }

        public void SendMessage(string senderId, string receiverId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Message content cannot be empty.");
                return;
            }

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content.Trim(),
                Timestamp = DateTime.Now,
                IsRead = false
            };

            _messages.Add(message);
            Console.WriteLine("Message sent successfully.");
        }
    }
}