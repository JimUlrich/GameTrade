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


namespace GameTrade.ViewModels
{
    public class LookupByTitleViewModel : AddGameViewModel
    {
        public bool QueryTooLong { get; set; }
        public string PlatformName { get; set; }
        public string GenreName { get; set; }
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
            
            var query = from g in xDoc.Descendants("Game")
                        where g.Element("id").Value == gameid
                        select new
                        {
                            title = g.Element("GameTitle").Value,
                            platform = g.Element("Platform").Value,
                            year = g.Element("ReleaseDate").Value,
                          //  genre = g.Element("genre").Value,
                        };
            foreach (var item in query)
            {
                Title = item.title;
                PlatformId = GetPlatformId(item.platform, context);
                Year = item.year.Substring(item.year.Length - 4);
                PlatformName = item.platform;
              //  GenreName = item.genre;
            }
            GameId = id;
            
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

        private int GetPlatformId(string platformName, GameTradeDbContext context)
        {
            try
            {
                Platform platform = context.Platforms.Single(p => p.Name == platformName);
                int platformId = platform.Id;
                return platformId;
            }
            catch (InvalidOperationException)
            {
                Platform newPlatform = new Platform
                {
                    Name = platformName
                };
                context.Platforms.Add(newPlatform);
                context.SaveChanges();

                return newPlatform.Id;
            }
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






