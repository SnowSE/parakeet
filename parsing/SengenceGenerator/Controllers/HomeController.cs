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
        private readonly ITrigramAdapter _adapter;

        public HomeController(ITrigramAdapter adapter)
        {
            _adapter = adapter;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DisplayWord(FormModel model)
        {
            var trigramService = new TrigramService();
            var startingWord = "this";

            //var unparsedDictonary = _adapt
            //Wait for database response
            var dictionary = _adapter.ConvertTrigramObjectToString();

            var sentence = trigramService.CreateSentenceFromTrigram(dictionary, startingWord);
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
