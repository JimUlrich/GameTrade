using System;
using System.Collections;
using GameTrade.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTrade.Models;
using Microsoft.EntityFrameworkCore;



namespace GameTrade.ViewModels
{
    public class ListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<GameGenre> GameGenres { get; set; }

        public ListViewModel() { }

        public ListViewModel(GameTradeDbContext context, string userId)
        {
            IEnumerable<Game> gameQuery = BuildGames(context, userId);
            Games = gameQuery;
            List<GameGenre> gameGenres = new List<GameGenre>();

            foreach (Game game in gameQuery)
            {
                var query = context.GameGenres.Where(g => g.GameId == game.GameId).Include(g => g.Genre);
                foreach (var item in query)
                {
                    gameGenres.Add(item);
                }
            }
            GameGenres = gameGenres;
        }

        private IEnumerable<Game> BuildGames(GameTradeDbContext context, string userId)
        {
            IEnumerable<Game> query = context.Games.Where(g => g.UserId == userId)
                                      .Include(g => g.Platform)
                                      .Include(g => g.Condition)
                                      .Include(g => g.Designation);
            return query;
        }

    }
}
