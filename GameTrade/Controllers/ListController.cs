﻿using GameTrade.Data;
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
            ListViewModel newListViewModel =  new ListViewModel(context, userId);
             
            return View(newListViewModel);
        }

        [Authorize]
        public IActionResult Add()
        {
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
                string genres = addGameViewModel.GenreNames;
                int gameId = newGame.GameId;
                Models.Extensions.AddGenres(context, gameId, genres);

                return Redirect("/List?m=Game Successfully Added");
            }

            string genreIds = addGameViewModel.GenreIds;
            AddGameViewModel newAddGameViewModel = new AddGameViewModel(context, genreIds);
            return View(newAddGameViewModel);
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
    }
}
    
        //TODO: make list of genreids into a string and then split the string to get the ids
        //TODO: VIEW  only certain categories
        
       
        



    

