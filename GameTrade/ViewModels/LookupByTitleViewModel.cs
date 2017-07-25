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
            UserId = viewmodel.UserId;
            Games = GetGamesList(viewmodel.Title);
        }

        public LookupByTitleViewModel(ClaimsPrincipal user) 
        {
            UserId = Models.Extensions.GetUserID(user);
        }

        private XDocument GetGamesDBInfo(string title)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGamesList.php?name=" + title);

            //might be able to move this bit to avoid repetition

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
