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
using System.Reflection;

namespace GameTrade.ViewModels
{
    public class AddGameViewModel : GameViewModel
    {
       
        public List<SelectListItem> Conditions { get; set; }
        public List<SelectListItem> Designations { get; set; }
        public List<SelectListItem> Platforms { get; set; }
        public List<SelectListItem> Genres { get; set; }    

        public AddGameViewModel() { }     

        public AddGameViewModel(GameTradeDbContext context, string genreIds = null)
        {
            Platforms = BuildSelectListItem(Models.Extensions.GetDbSet(context, "Platforms"));
            Conditions = BuildSelectListItem(Models.Extensions.GetDbSet(context, "Conditions"));
            Designations = BuildSelectListItem(Models.Extensions.GetDbSet(context, "Designations"));
            Genres = BuildSelectListItem(Models.Extensions.GetDbSet(context, "Genres"));
            if (genreIds != null)
            {
                GenreNames = BuildGenres(context, genreIds);
                GenreIds = genreIds;
            }
        }

        private List<SelectListItem> BuildSelectListItem(IEnumerable dbSet)
        {
            List<SelectListItem> newList = new List<SelectListItem>();

            foreach (var item in dbSet)
            {
                newList.Add(new SelectListItem
                {
                    Text = item.GetType().GetProperty("Name").GetValue(item).ToString(),
                    Value = item.GetType().GetProperty("Id").GetValue(item).ToString()
                });
            }
            return newList;
        }



        

        
    }
}//TODO: implement multiple genre adds
