using System.Collections.Generic;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;

namespace CompartiMoss.BootCamp.Web.Services
{
    public interface IRevista
    {
        Task<Revista> GetLastNumber();

        Task<IEnumerable<Revista>> GetRevistas();
        Task<Revista> GetRevistaByTitle(string name);
    }
}
