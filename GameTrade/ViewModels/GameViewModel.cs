using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTrade.Models;
using System.ComponentModel.DataAnnotations;

namespace GameTrade.ViewModels
{
    public class GameViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal Value { get; set; }

        [Required]
        public string Condition { get; set; }

        public string GameID { get; set; }

        public string GamesdbID { get; set; }

        public string Description { get; set; }
    }
}
