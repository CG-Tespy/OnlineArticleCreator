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
using OnlineArticleCreator.Utils;

namespace OnlineArticleCreator.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        Gallery gallery;
        string galleryOwnerName;
        ISession session;

        public GalleriesController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult ViewGallery(string userName)
        {
            session = HttpContext.Session;

            if (string.IsNullOrEmpty(userName))
                if (UserLoggedIn())
                {
                    string loggedInUserName = session.GetString("userName");
                    return ViewGallery(loggedInUserName);
                }
                else
                {
                    return ToHomePage();
                }

            GetGalleryToDisplayFor(userName);
            ViewBag.userName = gallery.Account.UserName;
            return View(gallery);
        }

        bool UserLoggedIn()
        {
            return !string.IsNullOrEmpty(session.GetString("userName"));
        }

        IActionResult ToHomePage()
        {
            return RedirectToAction("Index", "Home");
        }

        void GetGalleryToDisplayFor(string userName)
        {
            var accounts = _context.Accounts
                                            .Include(acc => acc.Gallery)
                                                .ThenInclude(gall => gall.Articles)
                                            .ToList();

            var hasGallery = accounts.Where(a => a.UserName == userName).Single();
            gallery = hasGallery.Gallery;
        }

        public IActionResult ViewArticle(int id)
        {
            var articles = _context.Articles.ToList();
            var article = articles.Single(ar => ar.ID == id);

            // Get the article with the contents and name read from disk
            article = ArticleReader.ReadArticleFromDisk(article.FilePath);
            article.Account = _context.Accounts.Where(acc => acc.ID == article.AccountID).Single();
            return View(article);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "ID", gallery.AccountID);
            return View(gallery);
        }



    }
}
