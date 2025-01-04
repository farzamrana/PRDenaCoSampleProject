using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Grid
{
    public class GridModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "{0} is required")]
        public string Name { set; get; }
        public string AuthorName { set; get; }
        public int Category_Id { set; get; }
        public int AccountGroup_Id { set; get; }
        public int Status { set; get; }


    }
  
}
