using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace CompartiMoss.BootCamp.Web.Services
{
    public class RevistaService : IRevista
    {
        private IEnumerable<Revista> _repository;
        private readonly IConfigurationRoot configuration;
        private readonly bool canRedis;
        private readonly string urlRedis;
        public RevistaService()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            this.configuration = builder.Build();
            canRedis = configuration.Get<bool>("Compartimoss:Redis");
            if (canRedis)
            {
                urlRedis = configuration.Get<string>("Compartimoss:urlRedis");
                using (var redisClient = new RedisClient(urlRedis, 6379))
                {
                    _repository = redisClient.Get<IEnumerable<Revista>>("revista");
                }
            }
        }
        public async Task<Revista> GetLastNumber()
        {
            if (_repository == null)
            {
                var compartimossService = new CompartiMossService();
                _repository = await compartimossService.SearchNumbers();
                if (canRedis)
                {
                    using (var redisClient = new RedisClient(urlRedis, 6379))
                    {
                        redisClient.Set<IEnumerable<Revista>>("revista", _repository);
                    }
                }
            }
            return _repository.FirstOrDefault();
        }

        public async Task<IEnumerable<Revista>> GetRevistas()
        {
            if (_repository == null)
            {
                var compartimossService = new CompartiMossService();
                _repository = await compartimossService.SearchNumbers();
                if (canRedis)
                {
                    using (var redisClient = new RedisClient(urlRedis, 6379))
                    {
                        redisClient.Set<IEnumerable<Revista>>("revista", _repository);
                    }
                }
            }
            return _repository;
        }

        public async Task<Revista> GetRevistaByTitle(string name)
        {
            if (_repository == null)
            {
                var compartimossService = new CompartiMossService();
                _repository = await compartimossService.SearchNumbers();
                if (canRedis)
                {
                    using (var redisClient = new RedisClient(urlRedis, 6379))
                    {
                        redisClient.Set<IEnumerable<Revista>>("revista", _repository);
                    }
                }
            }
            return _repository.Where(x => x.Numero.Equals(name)).FirstOrDefault();
        }
    }
}
