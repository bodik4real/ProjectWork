using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoManager.UI.Models.Chat
{
    public class ChatModel
    {
        public List<ChatUser> Users;  //all users

        public List<ChatMessage> Messages; // all messages

        public ChatModel()
        {
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();

            Messages.Add(new ChatMessage()
            {
                Text = "Start chat: " + DateTime.Now
            });
        }
    }
}