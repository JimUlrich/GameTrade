using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameTrade.Data;
using GameTrade.Models;

namespace GameTrade.Controllers
{
    public class HomeController : Controller
    {
        private GameTradeDbContext context;

        public HomeController(GameTradeDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<ApplicationUser> users = context.Users.ToList();
            return View(users);
        }

       //TODO: implement "search by" function 

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
