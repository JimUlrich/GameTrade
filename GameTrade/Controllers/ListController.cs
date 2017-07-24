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
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Collections;
using System;

namespace GameTrade.Controllers
{
    
    public class ListController : Controller

    {
        private GameTradeDbContext context;

        public ListController(GameTradeDbContext dbContext)
        {
            context = dbContext;
        }

        [Authorize]
        public IActionResult Index(string m)
        {
            IEnumerable<Game> query = from g in context.Games
                        where g.UserId == Models.Extensions.GetUserID(User)
                        select g;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
                AddGameViewModel addGameViewModel = new AddGameViewModel(User);
                return View(addGameViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddGameViewModel addGameViewModel) 
        {
            if (ModelState.IsValid)
            {
                Game newGame = new Game(addGameViewModel);

                context.Games.Add(newGame);
                context.SaveChanges();

                return Redirect("/List?m=Game Successfully Added");
            }

           return View(addGameViewModel);
        }

        [Authorize]
        public IActionResult LookupByTitle()
        {
            LookupByTitleViewModel lookupByTitleViewModel = new LookupByTitleViewModel(User);
            return View(lookupByTitleViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult LookupByTitle(LookupByTitleViewModel lookupByTitleViewModel)
        {       
                
                LookupByTitleViewModel newLookupByTitleViewModel = new LookupByTitleViewModel(lookupByTitleViewModel.Title);
                return View(newLookupByTitleViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult LookedupGame(LookupByTitleViewModel lookupByTitleViewModel)
        {
            LookedupGameViewModel lookedupGameViewModel = new LookedupGameViewModel(lookupByTitleViewModel, User);
            return View(lookedupGameViewModel);
        }

        [Authorize]
        public IActionResult Remove()
        {
            IList<Game> games = context.Games.ToList();
            return View(games);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Edit(int ID)
        {
            Game gameToEdit = context.Games.Single(c => c.GameId == ID);
            EditGameViewModel editGameViewModel = new EditGameViewModel(gameToEdit);

            return View(editGameViewModel);
        }

        [Authorize]
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

        public IActionResult Game(int id)
        {
            Game gameToDisplay = context.Games.Single(g => g.GameId == id);
            GameViewModel gameViewModel = new GameViewModel(gameToDisplay);
            return View(gameViewModel);
        }

        public IActionResult UserList(string userId, string sort)
        {           
            var query = context.Games.Where(g => g.UserId == userId);
            var sortedQuery = query;
            if (sort != null)
            {
                sortedQuery = query.OrderBy(g => g.GetType().GetProperty(sort).GetValue(g));
            }
            
            Tuple<IEnumerable<Game>, string> tuple = new Tuple<IEnumerable<Game>, string>(sortedQuery, userId);
            return View(tuple);
        }
    }



}
    

        //TODO:  use gamesdb naming conventions,     Sort by users preference/bst
        
       
        



    

