using GameTrade.Data;
using GameTrade.Models;
using GameTrade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


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

            string userId = Models.Extensions.GetUserID(User);

            IList<Game> games = context.Games.ToList();
            return View(games);
        }

        public IActionResult Add()
        {
           AddGameViewModel addGameViewModel = new AddGameViewModel(User);
           return View(addGameViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddGameViewModel addGameViewModel) 
        {
            if (ModelState.IsValid)
            {
                Game newGame = new Game(addGameViewModel);

                context.Games.Add(newGame);
                context.SaveChanges();

                return Redirect("/List");
            }

            return View(addGameViewModel);
        }

        public IActionResult Add(LookedupGameViewModel lookedupGameViewModel)
        {
            AddGameViewModel addGameViewModel = new AddGameViewModel(lookedupGameViewModel);
            return View(addGameViewModel);
        }

        public IActionResult LookupByTitle()
        {
            LookupByTitleViewModel lookupByTitleViewModel = new LookupByTitleViewModel(User);
            return View(lookupByTitleViewModel);
        }

        [HttpPost]
        public IActionResult LookedupGame(LookupByTitleViewModel lookupByTitleViewModel)
        {
            string title = lookupByTitleViewModel.Title;
            string userId = lookupByTitleViewModel.UserId;
            LookedupGameViewModel lookedupGameViewModel = new LookedupGameViewModel(title, userId);
            return View(lookedupGameViewModel);
            
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
                Game thegame = context.Games.Single(g => g.GameId == gameId);
                context.Games.Remove(thegame);
            }

            context.SaveChanges();

            return Redirect("/List");
        }

        public IActionResult Edit(int ID)
        {
            Game gameToEdit = context.Games.Single(c => c.GameId == ID);
            EditGameViewModel editGameViewModel = new EditGameViewModel(gameToEdit);

            return View(editGameViewModel);

        }

        [HttpPost]
        public IActionResult Edit(EditGameViewModel editGameViewModel)
        {
            Game gameToEdit = context.Games.Single(g => g.GameId == editGameViewModel.GameID);

            if (ModelState.IsValid)
            {
                gameToEdit.Edit(editGameViewModel);
                context.SaveChanges();

                return Redirect("/List");
            }

            return View(editGameViewModel);
        }

        public IActionResult Game(int ID)
        {
            Game gameToDisplay = context.Games.Single(g => g.GameId == ID);
            GameViewModel gameViewModel = new GameViewModel(gameToDisplay);
            return View(gameViewModel);
        }
    }



}
    

        //TODO: is it better to break things up in to separate databases or to make more than 1 entry for every game?
        // should users be able to enter their own systems or select from a list?
       
      
        // use gamesdb naming conventions
        //Sort by users preference/bst
        
        //fix null Add action
        



    

