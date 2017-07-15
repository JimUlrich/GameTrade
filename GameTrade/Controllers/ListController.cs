using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameTrade.Data;
using GameTrade.Models;
using Microsoft.EntityFrameworkCore;
using GameTrade.ViewModels;
using System.Net;

namespace GameTrade.Controllers
{
    public class ListController : Controller

    {
        private GameTradeDbContext context;

        public ListController(GameTradeDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Game> games = context.Games.ToList();
            return View(games);
        }

        public IActionResult Add()
        {
            AddGameViewModel addGameViewModel = new AddGameViewModel();
            return View(addGameViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddGameViewModel addGameViewModel)
        {
            if (ModelState.IsValid)
            {
                // Condition newCondition = context.Conditions.Single(c => c.ID == addGameViewModel.Condition);

                Game newGame = new Game
                {
                    Title = addGameViewModel.Title,
                    System = addGameViewModel.Platform,
                    Value = addGameViewModel.Value,
                    Year = addGameViewModel.Year,
                    Description = addGameViewModel.Description,
                    GameCondition = addGameViewModel.Condition
                };

                context.Games.Add(newGame);
                context.SaveChanges();

                return Redirect("/List");
            }

            return View(addGameViewModel);
        }

        public IActionResult Remove()
        {
            IList<Game> games = context.Games.ToList();
            return View(games);
        }

        [HttpPost]
        public IActionResult Remove(int[] gameIds)
        {
            foreach (int gameId in gameIds)
            {
                Game thegame = context.Games.Single(g => g.GameID == gameId);
                context.Games.Remove(thegame);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        public IActionResult Edit(int ID)
        {
            Game gameToEdit = context.Games.Single(c => c.GameID == ID);
            EditGameViewModel editGameViewModel = new EditGameViewModel(gameToEdit);

            return View(editGameViewModel);

        }




    }
}
    

        //TODO: is it better to break things up in to separate databases or to make more than 1 entry for every game?
        // should users be able to enter their own systems or select from a list?
       
        // populate database beforehand - just create, migrate, fill it up and then compile
       // HttpWebRequest  //save the ID in a database
        // use gamesdb naming conventions
        //Sort by users preference


    

