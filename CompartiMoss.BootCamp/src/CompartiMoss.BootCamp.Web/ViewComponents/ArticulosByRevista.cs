using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

namespace CompartiMoss.BootCamp.Web.ViewComponents
{
    public class ArticulosByRevista : ViewComponent
    {
        private IArticulos _service;

        public ArticulosByRevista(IArticulos service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke(string name)
        {
            var articulosCollection = _service.GetArticulosByRevista(name);
            return View(articulosCollection);
        }
    }
}
