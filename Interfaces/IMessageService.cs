using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleChat.Models;

namespace ConsoleChat.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string senderId, string receiverId, string content);
        List<Message> GetInbox(string userId);
        List<Message> GetConversation(string userId, string user2Id);
        int GetUnreadCount(string userId);
    }
}