using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

namespace DotNet.CompartiMOSS.Controllers
{
    public class RevistaController : Controller
    {
        private IRevista _service;
        public RevistaController(IRevista service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var revistaCollection = _service.GetRevistas();
            return View(revistaCollection);
        }

        public IActionResult Detail(string name)
        {
            var revista = _service.GetRevistaByTitle(name);
            return View(revista);
        }
    }
}
