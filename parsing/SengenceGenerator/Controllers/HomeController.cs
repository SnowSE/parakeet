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
        public async Task<IActionResult> Index()
        {
            var titles = await _adapter.GetTitlesAsync();
            var model = new FormModel { Titles = titles };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DisplayWord(FormModel model)
        {
            var trigramService = new TrigramService();
            var startingWord = model.Word;

            //Wait for database response
            var dictionary = _adapter.ConvertTrigramObjectToString();

            var sentence = trigramService.CreateSentenceFromTrigram(dictionary, startingWord);
            ViewBag.Sentence = sentence;
            return PartialView("Result", sentence);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
