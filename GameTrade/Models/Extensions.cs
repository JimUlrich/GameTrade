using GameTrade.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;

namespace GameTrade.Models
{
    public static class Extensions
    {
        public static string GetUserID(ClaimsPrincipal user)
        {

            var claimsIdentity = (ClaimsIdentity)user.Identity;
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException();
            }
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            return userId;
        }

     
        public static IEnumerable<Game> GetSortedQuery(GameTradeDbContext context, string userId, string sort = "")
        {
            var query = context.Games.Where(g => g.UserId == userId).Include(g => g.Platform).Include(g => g.Condition).Include(g => g.Designation);
            if (sort != null && sort != "Platform")
            {
                var sortedQuery = query.OrderBy(g => g.GetType().GetProperty(sort).GetValue(g));
                return sortedQuery;
            }
            if (sort == "Platform")
            {
                var sortedQuery = query.OrderBy(g => g.Platform.Name);
                return sortedQuery;
            }
            else
            {
                return query;
            }
        }

        public static IEnumerable<Object> GetDbSet(GameTradeDbContext context, string dbSet)
        {
            var newDbSet = context.GetType().GetProperty(dbSet);
            return (IEnumerable<Object>)newDbSet.GetValue(context);
        }

        public static int GetPropId(string propName, GameTradeDbContext context, string dbSetType)
        {
            try
            {
                var dbSet = GetDbSet(context, dbSetType);
                var query = dbSet.Single(g => g.GetType().GetProperty("Name").GetValue(g).ToString() == propName);
                var itemId = query.GetType().GetProperty("Id").GetValue(query).ToString();
                return Int32.Parse(itemId);
            }
            catch (InvalidOperationException)
            {
                if (dbSetType == "Platforms")
                {
                    Platform newPlatform = new Platform
                    {
                        Name = propName
                    };
                    context.Platforms.Add(newPlatform);
                    context.SaveChanges();

                    return newPlatform.Id;
                }

                else
                {
                    Genre newGenre = new Genre
                    {
                        Name = propName
                    };
                    context.Genres.Add(newGenre);
                    context.SaveChanges();

                    return newGenre.Id;
                }
            }
        }

        public static string[] SplitGenreIds(string genreIds)
        {
            String[] genres = genreIds.Split(',');
            return genres;
        }

        public static int GenreNameToId (GameTradeDbContext context, string[] genreIdsSplit, int i)
        {
            Genre genreQuery = context.Genres.Where(g => g.Name == (genreIdsSplit[i])).First();
            int genre = Int32.Parse(genreQuery.GetType().GetProperty("Id").GetValue(genreQuery).ToString());
            return genre;
        }

        public static void AddGenres(GameTradeDbContext context, int gameId, string genreIds = null)
        {
            string[] genreIdsSplit = SplitGenreIds(genreIds);
            for (int i = 0; i < genreIdsSplit.Length; i++)
            {
                int genreId = GenreNameToId(context, genreIdsSplit, i);
                GameGenre newGameGenre = new GameGenre
                {
                    GameId = gameId,
                    GenreId = genreId
                };
                context.GameGenres.Add(newGameGenre);
                context.SaveChanges();
            }
        }
    }
}