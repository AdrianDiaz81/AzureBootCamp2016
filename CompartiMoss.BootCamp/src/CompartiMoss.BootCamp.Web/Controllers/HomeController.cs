using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

namespace CompartiMoss.BootCamp.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPartner _service;

        public HomeController(IPartner service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var partnerCollection = _service.GetPartners();
            return View(partnerCollection);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
