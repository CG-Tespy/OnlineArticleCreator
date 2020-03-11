using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OnlineArticleCreator.Models;

namespace OnlineArticleCreator.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(24, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 1)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 1)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public void SetFrom(Profile profile)
        {
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            UserName = profile.UserName;
            Email = profile.Email;
        }

        public static ProfileViewModel FromModel(Profile profile)
        {
            var newViewModel = new ProfileViewModel();
            newViewModel.SetFrom(profile);
            return newViewModel;
        }

    }
}
