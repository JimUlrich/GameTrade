using GameTrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

        public LookupByTitleViewModel() { }

        public LookupByTitleViewModel(LookupByTitleViewModel viewmodel)
        {
            Title = viewmodel.Title;
            Platform = viewmodel.Platform;
            Year = viewmodel.Year;
            UserId = viewmodel.UserId;
        }

        public LookupByTitleViewModel(string title)
        {
            Games = GetGamesList(title);

        }

        public LookupByTitleViewModel(ClaimsPrincipal user) 
        {
            UserId = Models.Extensions.GetUserID(user);
        }

        private XDocument GetGamesDBInfo(string title)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGamesList.php?name=" + title);
            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;
        }

        private List<SelectListItem> GetGamesList(string title)
        {
            XDocument xDoc = GetGamesDBInfo(title);

            List<SelectListItem> Games = new List<SelectListItem>();

            var query = from g in xDoc.Descendants("Game")
                        where g.Element("GameTitle").Value.ToLower() == title.ToLower()
                        select new
                        {
                            Title = g.Element("GameTitle").Value,
                            Platform = g.Element("Platform").Value,
                            Year = g.Element("ReleaseDate").Value,
                            GameId = g.Element("id").Value
                        };

            foreach (var item in query)
            {
                Games.Add(new SelectListItem
                {
                    Text = item.Title + " - " + item.Platform,
                    Value = item.GameId
                });
            }

            return Games;
        }
    }

   

    //TODO: create a temp object to hold games gotten from gamesdb and use them to populate a selectlistitem
    //TODO: modify view to allow platofrm entry



}
