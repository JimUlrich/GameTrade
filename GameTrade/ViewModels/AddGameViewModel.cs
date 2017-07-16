using GameTrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameTrade.ViewModels
{
    public class AddGameViewModel
    {
        internal List<string> conditions = new List<string> { "Mint", "Near Mint", "Excellent", "Good", "Fair", "Poor" };

        [Required]
        public string Title { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal Value { get; set; }

        [Required]
        public string Condition { get; set; }

        public string GameID { get; set; }

        public string GamesdbID { get; set; }

        public string Description { get; set; }

        public List<SelectListItem> GameConditions { get; set; }

        internal List<SelectListItem> BuildConditions()
        {
            int i = 0;
          
            List<SelectListItem> GameConditions = new List<SelectListItem>();

            foreach (string condition in conditions)
            {
                GameConditions.Add(new SelectListItem
                {
                    Text = conditions[i],
                    Value = conditions[i]
                });

                i++;
                   
            }

            return GameConditions;
        }

        
        public AddGameViewModel()
        {
            GameConditions = BuildConditions();
        }

        public AddGameViewModel(String title)
        {
            XDocument xDoc = GetGamesDBInfo(title);

            var query = from g in xDoc.Descendants("Game")
                        where g.Element("GameTitle").Value.ToLower() == title.ToLower()
                        select new
                        {
                            Platform = g.Element("Platform").Value,
                            Year = g.Element("ReleaseDate").Value,
                            GameID = g.Element("id").Value
                        };
            
            foreach (var item in query)
            {
                Platform = item.Platform;
                Year = item.Year;
                GameID = item.GameID;
            }

            Title = title;
            GameConditions = BuildConditions();
        }

        private XDocument GetGamesDBInfo(String title)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGamesList.php?name=" + title);
            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;
        }

        //TODO: make a regex for the Year property






    }
}
