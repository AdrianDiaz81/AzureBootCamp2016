using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;

namespace CompartiMoss.BootCamp.Web.Services
{
    public interface ICompartiMossService
    {
        Task<List<Revista>> SearchNumbers();
        Task<List<Autor>> SearchAuthors();
        Task<List<Articulos>> SearchArticles(string number);
        Task<List<Articulos>> SearchArticlesByAutor(string autor);
    }
}
