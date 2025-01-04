using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models
{
    public class MessageViewModel
    {
        public string Message { get; set; }
        public string Color { get; set; }
        public MessageViewModel()
        {
            Message = string.Empty;
            Color = "black";
        }
    }
}
