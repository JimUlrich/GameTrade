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
using GameTrade.Data;
using Microsoft.EntityFrameworkCore;

namespace GameTrade.ViewModels
{
    public class AddGameViewModel : GameViewModel
    {
        

        internal List<string> conditions = new List<string> { "Mint", "Near Mint", "Excellent", "Good", "Fair", "Poor" };
        internal List<string> designations = new List<string> { "For Sale", "Wanted", "For Trade" };

        public List<SelectListItem> GameConditions { get; set; }
        public List<SelectListItem> Designations { get; set; }
        public List<SelectListItem> Platforms { get; set; }

        public AddGameViewModel()
        {
            GameConditions = BuildSelectListItem(conditions);
            Designations = BuildSelectListItem(designations);
        }

        public AddGameViewModel(GameTradeDbContext context)
        {

        }

       
        
        private IList BuildList(GameTradeDbContext context)
        {
            IList items = context.Platforms.ToList();
            return items; 
        }

        private List<SelectListItem> BuildSelectListItem(List<string> list)
        {
            List<SelectListItem> newList = new List<SelectListItem>();

            foreach (var item in list)
            {
                newList.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return newList;
        }
    }
}
