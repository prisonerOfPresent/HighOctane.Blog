using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HighOctane.Blog.Models;
using HighOctane.Blog.Repositories;

namespace HighOctane.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IRepository repo { get; set; }

        public HomeController(ILogger<HomeController> logger , IRepository repo )
        {
            _logger = logger;
            this.repo = repo;
        }

        public IActionResult Index()
        {
            IEnumerable<Post> posts = repo.GetAll();
            return View( posts );
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
