using System.Collections.Generic;
using CompartiMoss.BootCamp.Web.Models;

namespace CompartiMoss.BootCamp.Web.Services
{
    public class PartnerService : IPartner
    {
        public IEnumerable<Partner> GetPartners()
        {
            return new Partner[]
            {
                new Partner { Image="http://compartimoss.com/PublishingImages/Partners/KWizCOM.jpg",Name="KwizCom", Url="http://kwizcom.com" },
                new Partner { Image="http://compartimoss.com/PublishingImages/Partners/Encamina.png",Name="Encamina", Url="http://encamina.com" },
                new Partner { Image="http://compartimoss.com/PublishingImages/Partners/mvp-cluster-250x250.png",Name="MVP Cluster", Url="http://mvpcluster.com" }
            };
        }
    }
}
