using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleChat.Models;

namespace ConsoleChat.Interfaces
{
    public interface IStorageService
    {
        void SaveData(List<User> users,List<Message> massages);
        (List<User> users, List<Message> messages) LoadDate();
    }
}