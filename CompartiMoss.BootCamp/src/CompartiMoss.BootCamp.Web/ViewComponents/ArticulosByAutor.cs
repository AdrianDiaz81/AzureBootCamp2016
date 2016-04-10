using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

namespace CompartiMoss.BootCamp.Web.ViewComponents
{
    public class ArticulosByAutor : ViewComponent
    {
        private IArticulos _service;

        public ArticulosByAutor(IArticulos service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke(string name)
        {
            var articulosCollection = _service.GetArticulosByAutor(name);
            return View(articulosCollection);
        }
    }
}
