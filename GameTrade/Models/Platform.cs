using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameTrade.Models
{
    public class Platform
    {
        public string Name { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
