using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MGC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace MGC.Controllers.Web
{
    public class AppController : Controller
    {
        private IDataRepository _repository;
        private IConfigurationRoot _config;

        public AppController(IConfigurationRoot config, IDataRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Gifts()
        {
            var gifts = _repository.GetAllGifts(this.User.Identity.Name);
            return View(gifts);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AddGift()
        {
            return View();
        }
    }
}
