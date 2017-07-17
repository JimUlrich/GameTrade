using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.Models
{
    public class UserList
    {
        public int UserID { get; set; }
        public ApplicationUser User { get; set; }

        public int GameID { get; set; }
        public Game Game { get; set; }
    }
}
