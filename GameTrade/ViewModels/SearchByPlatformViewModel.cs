using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.ViewModels
{
    public class SearchByPlatformViewModel
    {
        public List<SelectListItem> Platforms { get; set; }

        public SearchByPlatformViewModel() { }

        public SearchByPlatformViewModel(IEnumerable platforms)
        {
            Platforms = BuildPlatforms(platforms);
        }

        private List<SelectListItem> BuildPlatforms(IEnumerable platforms)
        {
            List<SelectListItem> platformlist = new List<SelectListItem>();

            foreach (var platform in platforms)
            {
                platformlist.Add(new SelectListItem
                {
                    Text = platform.ToString(),
                    Value = platform.ToString()
                }
                    );
            }

            return platformlist;
        }
    }
}
