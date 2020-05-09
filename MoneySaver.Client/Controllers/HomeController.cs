using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneSaver.Client.Models;
using MoneySaver.Client.Services.Contracts;

namespace MoneSaver.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRabbitMqManager _rabbitMqManager;

        public HomeController(ILogger<HomeController> logger, IRabbitMqManager rabManager)
        {
            _logger = logger;
            this._rabbitMqManager = rabManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //var num = new System.Random().Next(9000);
            //this._rabbitMqManager.Publish(
            //    new { 
            //    field1 = $"Hello-{num}", 
            //    field2 = $"rabbit-{num}" 
            //    }, 
            //    "test.exchange", 
            //    "topic", 
            //    "*.queue.durable.dotnetcore.#");

            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
