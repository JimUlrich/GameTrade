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

        public LookedupGameViewModel(LookupByTitleViewModel viewModel)
        {
            int id = viewModel.GameId;
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
                Year = item.year;
            }
            GameConditions = BuildConditions();
        }

        internal XDocument GetGameDBInfoById(int id)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGame.php?id=" + id);
            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;
        }
    }
}

   
          
