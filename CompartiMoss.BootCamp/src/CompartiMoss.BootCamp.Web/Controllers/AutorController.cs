using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CompartiMoss.BootCamp.Web.Controllers
{
    public class AutorController : Controller
    {
        private IAutor _service;

        public AutorController(IAutor service)
        {
            _service = service;
        }

        
        public IActionResult Index()
        {
            var autorCollection = _service.GetAllAutor();
            return View(autorCollection);
        }

        [HttpGet]
        public IActionResult Detail(string name)
        {
            var result = _service.GetAutorByName(name);
            return View(result);
        }

     
    }
}
