using GameTrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameTrade.Data;
using System.Reflection;
using System.Collections;
using System.Text;

namespace GameTrade.ViewModels
{
    public class LookupByTitleViewModel : AddGameViewModel
    {
        [Display(Name = "Platform")]
        public string PlatformName { get; set; }

        public bool QueryTooLong { get; set; }
        public List<SelectListItem> Games { get; set; }     
        public string GameList { get; set; }

        [Display(Name = "Please select a game")]
        public int GameId { get; set; }

        public LookupByTitleViewModel() { }

        public LookupByTitleViewModel(string title, string platform = null, string gameList = null)
        {
            XDocument xDoc = new XDocument();

            if (gameList == null)
            {
                xDoc = GetGamesDBInfo(title, "GetGamesList", "name");
            }
            else
            {
                xDoc = XDocument.Parse(gameList);
            }
            GameList = xDoc.ToString();
            Games = GetGamesList(title, xDoc, platform );
        }  

        public LookupByTitleViewModel (int id, GameTradeDbContext context)
        {
            string gameid = id.ToString();
            List<string> gameData = new List<string>();
            XDocument xDoc = GetGamesDBInfo(gameid, "GetGame", "id");

            var query = GameQuery(xDoc, gameid);
            var genreQuery = GenreQuery(xDoc);
            
            foreach (var item in query)
            {
                Title = item.GetType().GetProperty("title").GetValue(item).ToString();
                var getPlatform = item.GetType().GetProperty("platform").GetValue(item).ToString();
                PlatformId = Models.Extensions.GetPropId(getPlatform, context, "Platforms");
                PlatformName = getPlatform;
                var getYear = item.GetType().GetProperty("year").GetValue(item).ToString();
                Year = getYear.Substring(getYear.Length - 4);    
            }
           
            StringBuilder genres = new StringBuilder();

            for (int i = 0; i < genreQuery.Count; i++ )
            {
                if (i < (genreQuery.Count - 1))
                {
                    int genreId = Models.Extensions.GetPropId(genreQuery[i], context, "Genres");
                    genres.Append(genreId + ",");
                }
                else
                {
                    int genreId = Models.Extensions.GetPropId(genreQuery[i], context, "Genres");
                    genres.Append(genreId);
                }
            }

            GenreIds = genres.ToString();
            GameId = id;       
        }

        // query xdoc for title, platform and year attributes
        private IEnumerable GameQuery(XDocument xDoc, string gameid)
        {
            var query = from g in xDoc.Descendants("Game")
                        where g.Element("id").Value == gameid
                        select new
                        {
                            title = g.Element("GameTitle").Value,
                            platform = g.Element("Platform").Value,
                            year = g.Element("ReleaseDate").Value
                        };
            return query;
        }


        private List<string> GenreQuery(XDocument xDoc)
        {
            var genreQuery = xDoc.Descendants("Genres").
                             Elements("genre").
                             Select(g => g.Value).
                             ToList();
            return genreQuery;
        }

        private List<SelectListItem> GetGamesList(string title, XDocument XDoc, string platform = null )
        {
            QueryTooLong = false; 
            List<SelectListItem> Games = new List<SelectListItem>();
            List<SelectListItem> PlatformList = new List<SelectListItem>();
            List<string> platforms = new List<string>();

            var query = from g in XDoc.Descendants("Game")
                        where g.Element("GameTitle").Value.ToLower().Contains(title.ToLower()) &&
                        (g.Element("Platform").Value == platform || string.IsNullOrEmpty(platform))
                        select new
                        {
                            Title = g.Element("GameTitle").Value,
                            Platform = g.Element("Platform").Value,
                            GameId = g.Element("id").Value
                        };

            if (query.Count() == 0)
            {
                Games = null;
            }
            if (query.Count() > 10)
            {
                Games = null;
                QueryTooLong = true;

                foreach (var item in query)
                {
                    if (!platforms.Contains(item.Platform))
                    {
                        platforms.Add(item.Platform);
                    }
                }

                foreach (string system in platforms)
                {                
                        PlatformList.Add(new SelectListItem
                        {
                            Text = system,
                            Value = system
                        });
                    
                }
                Platforms = PlatformList;
            }
            else
            {
                foreach (var item in query)
                {
                    Games.Add(new SelectListItem
                    {

                        Text = item.Title + " - " + item.Platform,
                        Value = item.GameId
                    });
                }
            }
            return Games;
        }

        private XDocument GetGamesDBInfo(string title, string searchParam1, string searchParam2)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/" + searchParam1 + ".php?" + searchParam2 + "=" + title);
            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;
        }
    }
}
//TODO: REMOVE TITLE FROM PLATFORM LOOKUP VIEW







