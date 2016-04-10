using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CompartiMoss.BootCamp.Articulo.Controllers
{
    [Route("api/[controller]")]
    public class ArticuloController : Controller
    {
        private const string url = "http://www.compartimoss.com/_api/search/query?querytext=##query##&QueryTemplatePropertiesUrl='spfile://webroot/queryparametertemplate.xml'&clienttype='Custom'";
        // GET: api/values
        [HttpGet("{number}")]
        public async Task<List<Model.Articulos>> SearchArticles(string number)
        {
            try
            {
                var queryUrl = url.Replace("##query##", "'ContentTypeId:0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF3900242457EFB8B24247815D688C526CD44D00F13378E475791F478B08D72D8E52EDCE* MagazineNumber:" + number + "'");
                queryUrl += "&selectproperties='PublishingImage,Path,Url,Title,PublishingPageContentOWSHTML,MagazineNumber'&sortlist='NumberPublishDateOWSDATE:descending'&rowlimit=40";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                var response = await httpClient.GetStringAsync(queryUrl);

                var body = JObject.Parse(response);
                var results = body["d"]["query"]["PrimaryQueryResult"]["RelevantResults"]["Table"]["Rows"]["results"];

                var articulos = new List<Model.Articulos>();
                foreach (var result in results)
                {
                    var article = new Model.Articulos()
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



        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
