using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.Models
{
    public class Game
    {
        

        public string Title { get; set; }
        public string System { get; set; }
        public int Year { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string GameCondition { get; set; }
        public int GameID { get; set; }
        public int GamesdbID { get; set; }


//TODO: add tradeable designation and migrate


    }
}
