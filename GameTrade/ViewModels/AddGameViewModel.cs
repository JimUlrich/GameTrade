using GameTrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.ViewModels
{
    public class AddGameViewModel
    {
        internal List<string> conditions = new List<string> { "Mint", "Near Mint", "Excellent", "Good", "Fair", "Poor" };

        [Required]
        public string Title { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        [Range(1970,2017)]
        public int Year { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Value { get; set; }

        [Required]
        public string Condition { get; set; }

        public string Description { get; set; }

        public List<SelectListItem> GameConditions { get; set; }

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

        public AddGameViewModel()
        {
            GameConditions = BuildConditions();
        }


    }
}
