using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

namespace CompartiMoss.BootCamp.Web.ViewComponents
{
    public class LastNumber:ViewComponent
    {
        private IRevista _service;
        public LastNumber(IRevista service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            var lastNumber = _service.GetLastNumber();
            return View(lastNumber);
        }
    }
}
