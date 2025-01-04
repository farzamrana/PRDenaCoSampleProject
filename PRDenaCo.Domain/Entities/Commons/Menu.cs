using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Commons
{
    public class Menu
    {
        public int MenuOptions_Id { get; set; }
        public string MenuOptions_Title { get; set; }
        public string MenuOptions_Url { get; set; }
        public int MenuOptions_ParentId { get; set; }
    }
}
