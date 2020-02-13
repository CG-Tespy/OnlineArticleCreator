using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArticleCreator.Models.ViewModels
{
    public class LoginViewModel
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
