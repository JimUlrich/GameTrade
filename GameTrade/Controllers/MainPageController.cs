using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameTrade.Data;
using GameTrade.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameTrade.Controllers
{
    public class MainPageController : Controller
    {
        private GameTradeDbContext context;

        public MainPageController(GameTradeDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<ApplicationUser> users = context.Users.ToList();
            return View(users);
        }
    }
}
