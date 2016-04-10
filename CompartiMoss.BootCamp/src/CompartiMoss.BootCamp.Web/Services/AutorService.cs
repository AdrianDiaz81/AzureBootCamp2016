using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace CompartiMoss.BootCamp.Web.Services
{
    public class AutorService : IAutor
    {
        private IEnumerable<Autor> _collection;
        private bool canRedis ;
        private string urlRedis;
        private IConfigurationRoot configuration;
        public AutorService()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            this.configuration = builder.Build();
                 
            canRedis =configuration.Get<bool>("Compartimoss:Redis");
            if (canRedis)
            {
                urlRedis= configuration.Get<string>("Compartimoss:urlRedis");
                using (var redisClient = new RedisClient(urlRedis, 6379))
                {
                    _collection = redisClient.GetAll<Autor>().OrderBy(x => x.Name);

                }
            }

        }
        public async Task<IEnumerable<Autor>> GetAllAutor()
        {
            if (!_collection.Any())
            {
                var compartimossService = new CompartiMossService();
                _collection = await compartimossService.SearchAuthors();
                if (canRedis)
                {
                    using (var redisClient = new RedisClient(urlRedis, 6379))
                    {
                        redisClient.StoreAll(_collection);
                    }
                }
            }

            return _collection;


        }

        public async Task<Autor> GetAutorByName(string name)
        {
            if (!_collection.Any())
            {
                var compartimossService = new CompartiMossService();
                _collection = await compartimossService.SearchAuthors();
                if (canRedis)
                {

                    using (var redisClient = new RedisClient(urlRedis, 6379))
                    {
                        redisClient.StoreAll(_collection);
                    }
                }
            }
            return _collection.Where(x => x.Name.Equals(name)).FirstOrDefault();

        }
    }
}
