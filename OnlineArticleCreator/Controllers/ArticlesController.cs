using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineArticleCreator.Data;
using OnlineArticleCreator.Models;
using OnlineArticleCreator.Models.ViewModels;
using OnlineArticleCreator.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace OnlineArticleCreator.Controllers
{
    /*
     * Handles the creation and editing of articles.
     */ 
    public class ArticlesController : Controller
    {
        static string articleFolder = "D:/Documents/VS Projects2/OnlineArticleCreator" + 
            "/ArticleFiles/";
        static string fileExtension = "json";
        static string fileNameFormat = "{0}_{1}_{2}";
        ArticleWriteArgs writeArgs;
        Article articleHandled;

        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: After account login and registration are finalized, make this require
        // the user to be logged in to access.
        public IActionResult Create()
        {
            var articleViewModel = new ArticleViewModel();
            return View(articleViewModel);
        }

        [HttpPost]
        public IActionResult Create(ArticleViewModel viewModel)
        {
            if (SessionTimedOut())
                return ToHomePage();

            if (ModelState.IsValid)
            {
                CreateBaseArticleWith(viewModel);
                RegisterArticle();
                return ShowUserArticleInGallery();
            }

            return View(viewModel);
        }

        bool SessionTimedOut()
        {
            return HttpContext.Session.GetInt32("accountID") == null;
        }

        IActionResult ToHomePage()
        {
            return RedirectToAction("Index", "Home");
        }

        void CreateBaseArticleWith(ArticleViewModel viewModel)
        {
            articleHandled = viewModel.CreateModel();
        }

        void RegisterArticle()
        {
            PrepareArticleToBeWritten();
            _context.SaveChanges();
            WriteArticleToDisk();
        }

        void WriteArticleToDisk()
        {
            ArticleWriter.WriteToDisk(writeArgs);
        }

        void PrepareArticleToBeWritten()
        {
            GetArticleAUniqueID();
            AssignArticleToRightAccount();

            SetupWriteArgsForArticle();
            RegisterFilePathToArticle();
            
            SetArticleDateCreated();
        }

        void GetArticleAUniqueID()
        {
            _context.Add(articleHandled);
        }

        void AssignArticleToRightAccount()
        {
            int accountID = (int)HttpContext.Session.GetInt32("accountID");
            articleHandled.AccountID = accountID;
        }


        void SetupWriteArgsForArticle()
        {
            var fileName = DecideFileNameForArticle();
            writeArgs = new ArticleWriteArgs(articleHandled, fileName,
                articleFolder, fileExtension);
        }

        string DecideFileNameForArticle()
        {
            string userName = HttpContext.Session.GetString("userName");
            string timestampFormat = "{0}_{1}_{2}";
            DateTime now = DateTime.Now;
            string timestamp = string.Format(timestampFormat, now.Month, now.Day, now.Year);

            return string.Format(fileNameFormat, userName, articleHandled.Name, timestamp);
        }

        void RegisterFilePathToArticle()
        {
            articleHandled.FilePath = writeArgs.FullFilePath;
        }

        void SetArticleDateCreated()
        {
            articleHandled.CreatedOn = DateTime.Now;
        }

        void RegisterArticleInDatabase(Article article)
        {
            _context.Add(article);
        }

        IActionResult ShowUserArticleInGallery()
        {
            return RedirectToAction("ViewArticle", 
                "Galleries", new { id = articleHandled.ID });
        }

    }
}
