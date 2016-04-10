using CompartiMoss.BootCamp.Web.Services;
using Microsoft.AspNet.Mvc;

namespace CompartiMoss.BootCamp.Web.ViewComponents
{
    public class Partner:ViewComponent
    {
        private IPartner _service;
        public Partner(IPartner service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            var partners = _service.GetPartners();
            return View(partners);
        }
    }
}
