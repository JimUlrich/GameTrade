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

       


    }
}