using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysShop.Models;
using BethanysShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            };

            return View(homeViewModel);
        }
    }
}
