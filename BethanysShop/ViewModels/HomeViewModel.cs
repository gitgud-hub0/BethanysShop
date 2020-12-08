using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysShop.Models;

namespace BethanysShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
