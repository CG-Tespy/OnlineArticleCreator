using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArticleCreator.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name/Email")]
        public string UserNameOrEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
