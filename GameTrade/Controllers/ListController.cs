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
using Microsoft.EntityFrameworkCore;

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
            ViewBag.Message = m;
            string userId = Models.Extensions.GetUserID(User);

            IEnumerable<Game> query = context.Games.Where(g => g.UserId == userId)
                                      .Include(g => g.Platform)
                                      .Include(g => g.Condition)
                                      .Include(g => g.Designation);
            
                        
            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            List<Platform> platforms = context.Platforms.ToList();
            ViewBag.Platforms = platforms;
            List<Condition> conditions = context.Conditions.ToList();
            ViewBag.Conditions = conditions;
            List<Designation> designations = context.Designations.ToList();
            ViewBag.Designations = designations;
            List<Genre> genres = context.Genres.ToList();
            ViewBag.Genres = genres;

            AddGameViewModel addGameViewModel = new AddGameViewModel(context);
            return View(addGameViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddGameViewModel addGameViewModel) 
        {
            if (ModelState.IsValid)
            {
                Game newGame = new Game(addGameViewModel);
                newGame.UserId = Models.Extensions.GetUserID(User);

                context.Games.Add(newGame);
                context.SaveChanges();

                return Redirect("/List?m=Game Successfully Added");
            }

            List<Platform> platforms = context.Platforms.ToList();
            ViewBag.Platforms = platforms;
            List<Condition> conditions = context.Conditions.ToList();
            ViewBag.Conditions = conditions;
            List<Designation> designations = context.Designations.ToList();
            ViewBag.Designations = designations;

            return View(addGameViewModel);
        }

        [Authorize]
        public IActionResult LookupByTitle()
        {
            LookupByTitleViewModel lookupByTitleViewModel = new LookupByTitleViewModel();
            return View(lookupByTitleViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult LookupByTitle(LookupByTitleViewModel lookupByTitleViewModel)
        {
           
            if (lookupByTitleViewModel.Games == null && lookupByTitleViewModel.GameId == 0)
            {
                ViewBag.TitleErrorMessage = "Search did not return any results.  Please search again.";
                ViewBag.QueryLengthErrorMessage = "Search yielded too many results.  Please narrow by platform";

                string title = lookupByTitleViewModel.Title;
                string platform = lookupByTitleViewModel.PlatformName;
                string gameList = lookupByTitleViewModel.GameList;
                
                LookupByTitleViewModel newLookupByTitleViewModel = new LookupByTitleViewModel(title, platform, gameList);
                return View(newLookupByTitleViewModel);
            }
            else
            {
                LookupByTitleViewModel newLookupByTitleViewModel = new LookupByTitleViewModel(lookupByTitleViewModel.GameId, context);
                return View(newLookupByTitleViewModel);
            }
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
                var newgame = thegame.GetType().GetProperty("Platform").GetValue(thegame);
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
            if (userId == null)
            {
                return Redirect("/404");
            }
  
            IEnumerable<Game> sortedQuery = Models.Extensions.GetSortedQuery(context, userId, sort);
            Tuple<IEnumerable<Game>, string> tuple = new Tuple<IEnumerable<Game>, string>(sortedQuery, userId);
            return View(tuple); 
        }

        public IActionResult AddPlatform(string platformToQuery)
        {
            List<Platform> platforms = context.Platforms.ToList();


            List<string> platformNames = new List<string>();

            foreach (var platform in platforms)
            {
                platformNames.Add(platform.Name);
            }

            if (!platformNames.Contains(platformToQuery))
            {
                Platform newPlatform = new Platform
                {
                    Name = platformToQuery
                };

                context.Platforms.Add(newPlatform);
                context.SaveChanges();
            }

            return View();
        }
        

    }



}
    

        //TODO: VIEW  only
        
       
        



    

