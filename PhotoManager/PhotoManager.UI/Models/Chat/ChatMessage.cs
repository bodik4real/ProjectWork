using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoManager.UI.Models.Chat
{
    public class ChatMessage
    {
        public ChatUser User { get; set; }
        public DateTime Date = DateTime.Now;
        public string Text = "";
    }
}