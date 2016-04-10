using System.Collections.Generic;
using CompartiMoss.BootCamp.Web.Models;

namespace CompartiMoss.BootCamp.Web.Services
{
    public interface IPartner
    {
        IEnumerable<Partner> GetPartners();

    }
}
