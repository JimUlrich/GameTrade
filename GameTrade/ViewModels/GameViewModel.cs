using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTrade.Models;
using System.ComponentModel.DataAnnotations;
using GameTrade.Data;
using System.Reflection;

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

        [Required]
        [Display(Name = "Genre(s)")]
        public string GenreNames { get; set; }

        public int GameID { get; set; }
        public string Description { get; set; }
        public Game Game { get; set; }
        public string UserId { get; set; }
        public string GenreIds { get; set; }

        public GameViewModel() { }

        public GameViewModel(Game game)
        {
            Game = game;
        }

        internal string IntToGenreName(GameTradeDbContext context, string[] genreIdsSplit, int i)
        {
            Genre genreQuery = context.Genres.Where(g => g.Id == Int32.Parse(genreIdsSplit[i])).First();
            string genre = genreQuery.GetType().GetProperty("Name").GetValue(genreQuery).ToString();
            return genre;
        }

        internal string BuildGenres(GameTradeDbContext context, string genreIds = null)
        {
            string genres = "";
            string[] genreIdsSplit = Models.Extensions.SplitGenreIds(genreIds);
            for (int i = 0; i < genreIdsSplit.Length; i++)
            {
                if (i < (genreIdsSplit.Length - 1))
                {
                    string genre = IntToGenreName(context, genreIdsSplit, i);
                    genres += genre + ", ";
                }
                else
                {
                    genres += IntToGenreName(context, genreIdsSplit, i);
                }
            }
            return genres;
        }

        
    }
}

