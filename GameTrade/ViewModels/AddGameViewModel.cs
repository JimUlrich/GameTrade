using GameTrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameTrade.ViewModels
{
    public class AddGameViewModel : GameViewModel
    {
        internal List<string> conditions = new List<string> { "Mint", "Near Mint", "Excellent", "Good", "Fair", "Poor" };

        public List<SelectListItem> GameConditions { get; set; }
    
        public AddGameViewModel()
        {
            GameConditions = BuildConditions();
        }

        public AddGameViewModel(ClaimsPrincipal user)
        {
           
            UserId = Models.Extensions.GetUserID(user);
            GameConditions = BuildConditions();
        }

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


       

       






        //TODO: make a regex for the Year property
        //TODO: remove query from constructor






    }
}
