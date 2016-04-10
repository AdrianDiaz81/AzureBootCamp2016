using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CompartiMoss.BootCamp.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CompartiMoss.BootCamp.Web.Services
{
    public class CompartiMossService : ICompartiMossService
    {
        private const string url = "http://www.compartimoss.com/_api/search/query?querytext=##query##&QueryTemplatePropertiesUrl='spfile://webroot/queryparametertemplate.xml'&clienttype='Custom'";
        private string urlRevista;
        private string urlAutor;
        private string urlArticulo;

        public CompartiMossService()
        {
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json");             
            var configuration = builder.Build();
            urlRevista = configuration.Get<string>("Compartimoss:Revista");
            urlAutor = configuration.Get<string>("Compartimoss:Autor");
            urlArticulo = configuration.Get<string>("Compartimoss:Articulo");
        }
        public async Task<List<Revista>> SearchNumbers()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                var response = await httpClient.GetStringAsync( urlRevista+ "/api/revista");

                return JsonConvert.DeserializeObject<List<Revista>>(response);
            }
            catch (Exception e)
            {
                throw new Exception("Se ha producido un error en la carga de los números");
            }
        }

        public async Task<List<Autor>> SearchAuthors()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                var response = await httpClient.GetStringAsync(urlAutor+ "/api/autor");

                return JsonConvert.DeserializeObject<List<Autor>>(response);


            }
            catch (Exception e)
            {
                throw new Exception("Se ha producido un error en la carga de los autores");
            }
        }


        public async Task<List<Articulos>> SearchArticles(string number)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                var response = await httpClient.GetStringAsync( urlArticulo+"/api/articulo/" + number);

                return JsonConvert.DeserializeObject<List<Articulos>>(response);


            }
            catch (Exception)
            {
                throw new Exception("Se ha producido un error en la carga de los artículos");
            }

        }

        public async Task<List<Articulos>> SearchArticlesByAutor(string autor)
        {
            try
            {
                var queryUrl = url.Replace("##query##", "'ContentTypeId:0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF3900242457EFB8B24247815D688C526CD44D00F13378E475791F478B08D72D8E52EDCE* Autor:" + autor + "'");
                queryUrl += "&selectproperties='PublishingImage,Path,Url,Title,PublishingPageContentOWSHTML,MagazineNumber'&sortlist='NumberPublishDateOWSDATE:descending'&rowlimit=40";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                string response = await httpClient.GetStringAsync(queryUrl);

                JObject body = JObject.Parse(response);
                var results = body["d"]["query"]["PrimaryQueryResult"]["RelevantResults"]["Table"]["Rows"]["results"];

                var articulos = new List<Articulos>();
                foreach (var result in results)
                {
                    var article = new Articulos()
                    {
                        Titulo = result["Cells"]["results"][5]["Value"].ToString().Replace("CompartiMOSS\r\n            \r\n            \r\n            ", ""),
                        Texto = result["Cells"]["results"][6]["Value"].ToString(),
                        LinkUrl = result["Cells"]["results"][4]["Value"].ToString(),
                        NumeroRevista = result["Cells"]["results"][7]["Value"].ToString(),
                        Autor = result["Cells"]["results"][2]["Value"].ToString()
                    };

                    articulos.Add(article);
                }

                return articulos;

            }
            catch (Exception)
            {
                throw new Exception("Se ha producido un error en la carga de los artículos");
            }

        }

    }
}
