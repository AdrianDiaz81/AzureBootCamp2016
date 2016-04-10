using System.Collections.Generic;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;

namespace CompartiMoss.BootCamp.Web.Services
{
    public interface IAutor
    {
        Task<IEnumerable<Autor>> GetAllAutor();
        Task<Autor> GetAutorByName(string name);
    }
}
