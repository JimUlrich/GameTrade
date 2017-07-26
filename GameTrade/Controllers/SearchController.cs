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
            SearchByViewModel searchByViewModel = new SearchByViewModel();
            return View(searchByViewModel);
        }

        [HttpPost]
        public IActionResult ByTitle(SearchByViewModel searchByViewModel)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Game> games = context.Games.Where(g => g.Title == searchByViewModel.Title);
                List<string> gameTitles = new List<string>();
         
                foreach (var game in games)
                {
                    gameTitles.Add(game.Title);
                }

                if (gameTitles.Contains(searchByViewModel.Title))
                {
                    SearchByViewModel viewModel = new SearchByViewModel(games);
                    return View(viewModel);
                }
                else
                {
                    ViewBag.ErrorMessage = "No games were found with that title.  Please search again.";
                    return View(searchByViewModel);
                }
            }

            return View(searchByViewModel);
        }

        public IActionResult ByPlatform()
        {
            IEnumerable platforms =
                from p in context.Games
                select p.System;

            SearchByViewModel searchByViewModel = new SearchByViewModel(platforms);
            return View(searchByViewModel);
        }

        [HttpPost]
        public IActionResult ByPlatform(SearchByViewModel searchByViewModel)
        {
            IEnumerable<Game> games = context.Games.Where(g => g.System == searchByViewModel.Platform);
            SearchByViewModel viewModel = new SearchByViewModel(games);
            return View(viewModel);
        }



       
            
    }
}
