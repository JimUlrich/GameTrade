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

namespace GameTrade.ViewModels
{
    public class LookupByTitleViewModel : AddGameViewModel
    {
        public List<SelectListItem> Games { get; set; }

        [Display(Name = "Please select a game")]
        public int GameId { get; set; }

        public LookupByTitleViewModel() { }

        public LookupByTitleViewModel(LookupByTitleViewModel viewmodel)
        {
            Title = viewmodel.Title;
            Platform = viewmodel.Platform;
            Year = viewmodel.Year;
            GameId = viewmodel.GameId;
        }

        public LookupByTitleViewModel (string title)
        {
            Games = GetGamesList(title);
        }

        public LookupByTitleViewModel (int id)
        {            
            List<string> gameData = new List<string>();
            XDocument xDoc = GetGameDBInfoById(id);
            string gameid = id.ToString();

            var query = from g in xDoc.Descendants("Game")
                        where g.Element("id").Value == gameid
                        select new
                        {
                            title = g.Element("GameTitle").Value,
                            platform = g.Element("Platform").Value,
                            year = g.Element("ReleaseDate").Value,
                        };
            foreach (var item in query)
            {
                Title = item.title;
                Platform = item.platform;
                Year = item.year.Substring(item.year.Length - 4); ;
            }
            GameId = id;
        }

        private XDocument GetGamesDBInfo(string title)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGamesList.php?name=" + title);

            //might be able to move this bit to avoid repetition

            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;
        }

        private XDocument GetGameDBInfoById(int id)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGame.php?id=" + id);
            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;
        }

        private List<SelectListItem> GetGamesList(string title, string platform = null, string year = null)  
        {
            
            XDocument xDoc = GetGamesDBInfo(title);

            List<SelectListItem> Games = new List<SelectListItem>();

            var query = from g in xDoc.Descendants("Game")
                        where g.Element("GameTitle").Value.ToLower() == title.ToLower() &&
                        (g.Element("Platform").Value == platform || string.IsNullOrEmpty(platform)) &&
                        (g.Element("ReleaseDate").Value.Substring(g.Element("ReleaseDate").Value.Length - 4) == year || string.IsNullOrEmpty(year))
                        select new
                        {
                            Title = g.Element("GameTitle").Value,
                            Platform = g.Element("Platform").Value,
                            Year = g.Element("ReleaseDate").Value,
                            GameId = g.Element("id").Value
                        };

            if (query.Count() == 0)
            {
                Games = null;
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
    }

   

    
    //TODO: modify view to allow platofrm entry



}
