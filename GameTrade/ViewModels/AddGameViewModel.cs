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
        internal List<string> designations = new List<string> { "For Sale", "Wanted", "For Trade" };

        public List<SelectListItem> GameConditions { get; set; }
        public List<SelectListItem> Designations { get; set; }

        public AddGameViewModel()
        {
        GameConditions = BuildSelectListItem(conditions);
        Designations = BuildSelectListItem(designations);
        }

        internal List<SelectListItem> BuildSelectListItem(List<string> listField)
        {
            List<SelectListItem> newList = new List<SelectListItem>();

            foreach (string item in listField)
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
