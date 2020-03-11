using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OnlineArticleCreator.Models.ViewModels;

namespace OnlineArticleCreator.Models
{
    public class Profile
    {
        [Key]
        public int AccountID { get; set; }

        public Account Account { get; set; }
        public Gallery Gallery
        {
            get { return Account.Gallery; }
        }

        [NotMapped]
        public string FirstName
        {
            get { return Account.FirstName; }
            set { Account.FirstName = value; }
        }

        [NotMapped]
        public string LastName 
        { 
            get { return Account.LastName; }
            set { Account.LastName = value; }
        }

        [NotMapped]
        public string UserName
        {
            get { return Account.UserName; }
            set { Account.UserName = value; }
        }

        [NotMapped]
        public string Email
        {
            get { return Account.Email; }
            set { Account.Email = value; }
        }

        public void SetFrom(ProfileViewModel viewModel)
        {
            this.FirstName = viewModel.FirstName;
            this.LastName = viewModel.LastName;
            this.UserName = viewModel.UserName;
            this.Email = viewModel.Email;
        }
    }
}
