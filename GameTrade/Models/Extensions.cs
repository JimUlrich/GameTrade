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
    }
}