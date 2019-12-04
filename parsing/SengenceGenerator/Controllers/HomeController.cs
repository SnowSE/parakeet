using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SentenceGenerator.Models;
using SentenceGenerator.Services;
using Shared.Data;

namespace SentenceGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrigramService _trigramService;
        private readonly ITrigramAdapter _adapter;

        public HomeController(ITrigramAdapter adapter, ITrigramService trigramService)
        {
            _trigramService = trigramService;
            _adapter = adapter;
        }
        public IActionResult Index()
        {
            ViewBag.TitleList = _adapter.GetTitles();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DisplayWord(FormModel model)
        {
            var startingWord = model.Word;

            //Wait for database response
            var dictionary = _adapter.ConvertTrigramObjectToString();

            var sentence = _trigramService.CreateSentenceFromTrigram(dictionary, startingWord);
            ViewBag.Sentence = sentence;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
