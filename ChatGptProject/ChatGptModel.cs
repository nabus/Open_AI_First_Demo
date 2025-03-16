using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_AI_Project
{
    public class ChatGptModel
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
    }
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
