using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SentenceGenerator.Models;
using SentenceGenerator.Services;

namespace SentenceGenerator.Controllers
{
    public class HomeController : Controller
    {
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
            //TESTING DATA
            string fileName = model.Title_;
            string text = System.IO.File.ReadAllText(@"wwwroot/titles/" + fileName);
            var dictionary = trigramService.getTrigramsDictionary(text);
            var startingWord = model.Word;
            ////////////////////////////

            //Wait for database response
            //var dictionary = _contextCallDatabase;

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
