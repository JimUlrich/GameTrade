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
        public string Platform { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public List<SelectListItem> Platforms { get; set; }

        public SearchByViewModel() { }

        public SearchByViewModel (IEnumerable platforms)
        {
            Platforms = BuildPlatforms(CreatePlatformList(platforms));
        }

        public SearchByViewModel (IEnumerable<Game> games)
        {
            Games = games;
        }

        private List<SelectListItem> BuildPlatforms(List<string> platforms)
        {
            List<SelectListItem> platformlist = new List<SelectListItem>();

            foreach (var platform in platforms)
            {
                platformlist.Add(new SelectListItem
                {
                    Text = platform,
                    Value = platform
                }
                    );
            }
            return platformlist;
        }

        private List<string> CreatePlatformList(IEnumerable allPlatforms)
        {
            List<string> platforms = new List<string>();

            foreach (string platform in allPlatforms)
            {
                if (!platforms.Contains(platform))
                {
                    platforms.Add(platform);
                }
            }

            return platforms;
        }
    }
}
