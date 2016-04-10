using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;
using Microsoft.Extensions.Configuration;

namespace CompartiMoss.BootCamp.Web.Services
{
    public class ArticuloService : IArticulos
    {
        private IEnumerable<Articulos> _repository;
        private IConfigurationRoot configuration;

        public ArticuloService()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            this.configuration = builder.Build();
            _repository = new Articulos[] { };

        }
        public async Task<IEnumerable<Articulos>> GetArticulosByAutor(string autor)
        {
            var compartimossService = new CompartiMossService();
            _repository = await compartimossService.SearchArticlesByAutor(autor);
            return _repository;
        }

        public async Task<IEnumerable<Articulos>> GetArticulosByRevista(string revista)
        {
            var compartimossService = new CompartiMossService();
            _repository = await compartimossService.SearchArticles(revista);
            return _repository.Where(x => x.NumeroRevista.Equals(revista));
        }
    }
}
