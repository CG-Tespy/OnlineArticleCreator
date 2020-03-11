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

namespace OnlineArticleCreator.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        Profile profile;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profiles
        public IActionResult Index(string userName)
        {
            var account = FindAccountTiedTo(userName);

            if (account == null)
                return NotFound();

            var viewModel = ProfileViewModel.FromModel(account.Profile);

            return View(viewModel);
        }

        Account FindAccountTiedTo(string userName)
        {
            var account = _context.Accounts
                .Where(acc => acc.UserName == userName)
                .Include(acc => acc.Profile)
                .SingleOrDefault();

            return account;
        }

        // GET: Profiles/Edit/5
        public IActionResult Edit()
        {
            if (!LoggedIn())
                return ToHomePage();
            
            string userName = HttpContext.Session.GetString("userName");
            GetProfileByUserName(userName);

            var viewModel = ProfileViewModel.FromModel(profile);
            
            return View(viewModel);
        }

        bool LoggedIn()
        {
            string userName = HttpContext.Session.GetString("userName");
            return !string.IsNullOrEmpty(userName);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileViewModel viewModel)
        {
            string email = viewModel.Email;
            GetProfileByEmail(email);
            viewModel.UserName = profile.UserName;
            profile.SetFrom(viewModel);
            _context.Update(profile.Account);
            _context.SaveChanges();

            ViewBag.savedChanges = true;
            return View(viewModel);
        }

        IActionResult ToHomePage()
        {
            return RedirectToAction("Index", "Home");
        }

        void GetProfileByUserName(string user)
        {
            var account = _context.Accounts
                            .Where(acc => acc.UserName == user)
                            .Include(acc => acc.Profile)
                            .Single();
            profile = account.Profile;
        }

        void GetProfileByEmail(string email)
        {
            var account = _context.Accounts
                            .Where(acc => acc.Email == email)
                            .Include(acc => acc.Profile)
                            .Single();
            profile = account.Profile;
        }
    }
}
