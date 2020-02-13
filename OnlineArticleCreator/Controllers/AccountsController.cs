using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineArticleCreator.Data;
using OnlineArticleCreator.Models;
using OnlineArticleCreator.Models.ViewModels;
using System.Security.Cryptography;
using System.Text;
using CGT.AspNetMvc;

namespace OnlineArticleCreator.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected static SHA512 passHasher = new SHA512Managed();
        protected static Encoding hashEncoding = Encoding.Unicode;
        AccountViewModel accountDetails;
        Account account;
        Profile profile;
        Gallery gallery;

        LoginViewModel loginInput;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Sign Up
        public IActionResult SignUp()
        {
            var viewModel = new AccountViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SignUp(AccountViewModel registrationInput)
        {
            accountDetails = registrationInput;

            if (ModelState.IsValid)
            {
                if (AccountAlreadyRegistered())
                {
                    LetUserKnow();
                }
                else
                {
                    RegisterAccount();
                    SetupProfileAndGallery();
                    RegisterUserAsLoggedInTo(account);
                    return ToHomePage();
                }
            }

            return View(registrationInput);
        }

        bool AccountAlreadyRegistered()
        {
            var email = accountDetails.Email.ToLower();
            var userName = accountDetails.UserName.ToLower();
            var accounts = _context.Accounts.ToList();

            foreach (var account in accounts)
            {
                if (email == account.Email.ToLower()
                    || userName == account.UserName.ToLower())
                    return true;
            }

            return false;
        }

        void LetUserKnow()
        {
            string errorKey = "alreadyRegistered";
            string errorMessage = "There's already an account with that email or username.";
            ModelState.AddModelError(errorKey, errorMessage);
        }

        void RegisterAccount()
        {
            account = accountDetails.CreateModel(passHasher, hashEncoding);
            _context.Add(account);
            _context.SaveChanges();
        }

        void SetupProfileAndGallery()
        {
            profile = new Profile();
            gallery = new Gallery();
            SyncProfileAndGalleryWithAccount();
            RegisterProfileAndGalleryToDatabase();
        }

        void SyncProfileAndGalleryWithAccount()
        {
            profile.AccountID = gallery.AccountID = account.ID;
        }

        void RegisterProfileAndGalleryToDatabase()
        {
            _context.Add(profile);
            _context.Add(gallery);
            _context.SaveChanges();
        }

        #endregion

        void RegisterUserAsLoggedInTo(Account account)
        {
            HttpContext.Session.SetString("userName", account.UserName);
            HttpContext.Session.SetInt32("accountID", account.ID);
        }

        IActionResult ToHomePage()
        {
            return RedirectToAction("Index", "Home");
        }

        #region Login
        public IActionResult Login()
        {
            var loginFields = new LoginViewModel();
            return View(loginFields);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginFields)
        {
            loginInput = loginFields;

            if (ModelState.IsValid && LoginFieldsAreCorrect())
            {
                RegisterUserAsLoggedInTo(account);
                return ToHomePage();
            }

            return View(loginFields);
        }

        bool LoginFieldsAreCorrect()
        {
            account = FindAccountMatchingLoginFields();
            return AccountFound();
        }
        
        Account FindAccountMatchingLoginFields()
        {
            var password = loginInput.Password.Hashed(passHasher, hashEncoding);
            // ^To avoid having to hash the input password more than once per login attempt

            foreach (var account in _context.Accounts.ToList())
            {
                if (IdentityMatches(account) && PasswordsMatch(password, account.Password))
                    return account;
            }

            return null;
        }

        bool IdentityMatches(Account account)
        {
            string identity = loginInput.UserNameOrEmail;
            StringComparison caseInsensitive = StringComparison.OrdinalIgnoreCase;

            return  identity.Equals(account.UserName, caseInsensitive) ||
                    identity.Equals(account.Email, caseInsensitive);
        }

        bool PasswordsMatch(string passwordInput, string accountPassword)
        {
            return passwordInput.Equals(accountPassword);
        }

        bool AccountFound()
        {
            return account != null;
        }

        #endregion

        #region Logout
        public IActionResult LogOut()
        {
            if (UserLoggedIn())
                LogUserOut();
            
            return ToHomePage();
        }

        bool UserLoggedIn()
        {
            string userName = HttpContext.Session.GetString("userName");
            return !string.IsNullOrEmpty(userName);
        }

        void LogUserOut()
        {
            HttpContext.Session.SetString("userName", "");
        }
        #endregion

    }
}
