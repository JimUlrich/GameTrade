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
        [Display(Name = "Platform")]
        public int PlatformId { get; set; }

        [Required]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Year must be entered in XXXX format")]
        public string Year { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal Value { get; set; }
   
        [Required]
        [Display(Name = "Condition")]
        public int ConditionId { get; set; }

        [Required]
        [Display(Name = "Buy/Sell")]
        public int DesignationId { get; set; }


        public int GameID { get; set; }
        public string GamesdbID { get; set; }
        public string Description { get; set; }
        public Game Game { get; set; }
        public string UserId { get; set; }
    
        public string Genre { get; set; }

        public GameViewModel() { }

        public GameViewModel(Game game)
        {
            Game = game;
        }


    }
}

