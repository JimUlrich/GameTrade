using GameTrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.ViewModels
{
    public class SearchByViewModel
    {
        [Required]
        [Display(Name ="Please enter a title")]
        public string Title { get; set; }

        [Display(Name = "")]
        public int PlatformId { get; set; }

        public IEnumerable<Game> Games { get; set; }
       
        public SearchByViewModel() { }

        public SearchByViewModel (IEnumerable<Game> games)
        {
            Games = games;
        }
    }
}
