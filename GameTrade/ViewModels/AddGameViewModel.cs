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

        public AddGameViewModel(GameTradeDbContext context, string genreIds = "1,2,3")
        {
            Platforms = BuildSelectListItem(GetDbSet(context, "Platforms"));
            Conditions = BuildSelectListItem(GetDbSet(context, "Conditions"));
            Designations = BuildSelectListItem(GetDbSet(context, "Designations"));
            Genres = BuildSelectListItem(GetDbSet(context, "Genres"));
            GenreIds = BuildGenres(context, genreIds);
        }
       
        internal IEnumerable<Object> GetDbSet(GameTradeDbContext context, string dbSet)
        {
            var newDbSet = context.GetType().GetProperty(dbSet);
            return (IEnumerable<Object>)newDbSet.GetValue(context);
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
