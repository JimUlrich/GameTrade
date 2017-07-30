using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTrade.Models;


namespace GameTrade.ViewModels
{
    public class EditGameViewModel : AddGameViewModel
    {
        public EditGameViewModel() { }

        public EditGameViewModel(Game game)
        {
            Title = game.Title;
            PlatformId = game.PlatformId;
            Value = game.Value;
            Year = game.Year;
            Description = game.Description;
            ConditionId = game.ConditionId;

            Game = game;
        }
    }
}
