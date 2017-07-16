using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTrade.Models;


namespace GameTrade.ViewModels
{
    public class EditGameViewModel : AddGameViewModel
    {
        public Game Game { get; set; }
        public int GameID { get; set; }

        public EditGameViewModel() { }

        public EditGameViewModel(Game game)
        {
            Title = game.Title;
            Platform = game.System;
            Value = game.Value;
            Year = game.Year;
            Description = game.Description;
            Condition = game.GameCondition;

            Game = game;
        }
    }
}
