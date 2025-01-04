using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Auth
{
    public class ForgotModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your {0}")]
        [EmailAddress(ErrorMessage = "The email must be a valid {0}.")]
        public string Email { get; set; }
    }
}
