using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using TrygramParserAPI.Models;
using TrygramParserAPI.Services;


namespace TrygramParserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrygramController : ControllerBase
    {
        private readonly ITrygramService _trygramService;

        public TrygramController(ITrygramService trygramService)
        {
            _trygramService = trygramService;
        }
        [HttpPost("[Action]")]
        public async Task CreateTrygrams([FromBody] RequestModel request)
        {
            await _trygramService.ParseAndPersistAsync(request.Title, request.Text);
        }
    }
}