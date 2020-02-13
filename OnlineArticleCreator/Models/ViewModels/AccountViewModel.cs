using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OnlineArticleCreator.Models;
using System.Security.Cryptography;
using CGT.AspNetMvc;

namespace OnlineArticleCreator.Models.ViewModels
{
    public class AccountViewModel
    {
        [Required]
        [StringLength(24, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 1)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string Password { get; set; }

        public Account CreateModel(HashAlgorithm hashAlgorithm, Encoding encoding)
        {
            return new Account
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Password = Password.Hashed(hashAlgorithm, encoding)
            };
        }
    }
}
