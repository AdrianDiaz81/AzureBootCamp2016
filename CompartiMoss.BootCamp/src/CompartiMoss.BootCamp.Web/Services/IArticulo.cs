using System.Collections.Generic;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;

namespace CompartiMoss.BootCamp.Web.Services
{
    public interface IArticulos
    {
        Task<IEnumerable<Articulos>> GetArticulosByAutor(string autor);
        Task<IEnumerable<Articulos>> GetArticulosByRevista(string revista);
    }
}
