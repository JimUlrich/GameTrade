using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTrade.ViewModels;

namespace GameTrade.Models
{
    public class Game
    {
        public string Title { get; set; }
        public string System { get; set; }
        public string Year { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string GameCondition { get; set; }
        public string Designation { get; set; }
        public int GameId { get; set; }
      //  public string Username { get; set; }
        public string UserId { get; set; }
        public string Genre { get; set; }

        //TODO: implement  and genre


        public Game(AddGameViewModel viewModel)
        {
            Title = viewModel.Title;
            System = viewModel.Platform;
            Value = viewModel.Value;
            Year = viewModel.Year;
            Description = viewModel.Description;
            GameCondition = viewModel.Condition;
            UserId = viewModel.UserId;
            Designation = viewModel.Designation;
        }

        public Game() { }

        public void Edit(EditGameViewModel viewModel)
        {
            Title = viewModel.Title;
            System = viewModel.Platform;
            Value = viewModel.Value;
            Year = viewModel.Year;
            Description = viewModel.Description;
            GameCondition = viewModel.Condition;
            Designation = viewModel.Designation;
        }

//TODO: make year an int or make regex
    }
}
