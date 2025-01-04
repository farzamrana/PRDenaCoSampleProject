using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Companies
{
    public class CurrentDateModel
    {
        [Display(Name = "Current Date")]
        [Required(ErrorMessage = "{0} is required")]
        public string  CurrentDate { get; set; }
    }
}
