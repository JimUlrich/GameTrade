using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.Models
{
    public class Platform : IComparable<Platform>
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int CompareTo(Platform that)
        {
            return Id.CompareTo(that.Id);
        }
    }
}
