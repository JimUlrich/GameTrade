using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameTrade.Models
{
    public static class Extensions
    {
        public static string GetUserID(ClaimsPrincipal user)
            {
                var claimsIdentity = (ClaimsIdentity)user.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                return userId;
            }

        public static XDocument GetGameDBInfoById(int id)
        {
            WebRequest gamesdbRequest = WebRequest.Create("http://thegamesdb.net/api/GetGame.php?id=" + id);
            WebResponse gamesdbResponse = gamesdbRequest.GetResponseAsync().Result;
            XDocument gamesdbXdoc = XDocument.Load(gamesdbResponse.GetResponseStream());
            return gamesdbXdoc;

        }

        public static IEnumerable SelectSingleGame(XDocument xDoc)
        {
            var query = from g in xDoc.Descendants("Data")
                        select new
                        {
                            Title = g.Element("GameTitle").Value,
                            Platform = g.Element("Platform").Value,
                            Year = g.Element("ReleaseDate").Value,
                        };

            return query;
             
        }


    }
}