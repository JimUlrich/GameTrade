using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameTrade.Data;
using GameTrade.Models;
using System.Collections;
using GameTrade.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameTrade.Controllers
{
    public class SearchController : Controller
    {
        private GameTradeDbContext context;

        public SearchController(GameTradeDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ByUser()
        {
            IList<ApplicationUser> users = context.Users.ToList();
            return View(users);
        }

        public IActionResult ByTitle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ByTitle()
        {
            return View();
        }

        public IActionResult ByPlatform()
        {
            IEnumerable platforms =
                from p in context.Games
                select p.System;

            SearchByPlatformViewModel searchByPlatformViewModel = new SearchByPlatformViewModel(platforms);
            return View(searchByPlatformViewModel);
        }



       
            
    }
}
