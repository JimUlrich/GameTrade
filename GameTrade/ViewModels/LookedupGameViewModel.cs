using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameTrade.ViewModels
{
    public class LookedupGameViewModel : LookupByTitleViewModel
    {
        public LookedupGameViewModel() { }

        public LookedupGameViewModel(string title, string userId)
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

            UserId = userId;
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
    }

      
}
