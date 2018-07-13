using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoManager.UI.Models.Chat
{
    public class ChatUser
    {
        public string Name { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastPing { get; set; }
    }
}