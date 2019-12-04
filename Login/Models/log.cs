using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class log
    {
        [Required(ErrorMessage = "Emailid is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Emailid { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}