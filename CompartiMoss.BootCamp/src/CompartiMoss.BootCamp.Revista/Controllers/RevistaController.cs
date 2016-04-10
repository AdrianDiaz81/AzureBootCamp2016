using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CompartiMoss.BootCamp.Revista.Controllers
{
    [Route("api/[controller]")]
    public class RevistaController : Controller
    {
        private const string url = "http://www.compartimoss.com/_api/search/query?querytext=##query##&QueryTemplatePropertiesUrl='spfile://webroot/queryparametertemplate.xml'&clienttype='Custom'";
        // GET: api/values
        [HttpGet]
        public async Task<List<Model.Revista>> SearchNumbers()
        {

            try
            {
                var queryUrl = url.Replace("##query##", "'ContentTypeId:0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF3900242457EFB8B24247815D688C526CD44D007EC555B0C95F3146BBBC6A377D640C82*'");
                queryUrl += "&selectproperties='PublishingImage,Path,Url,Title,PublishingPageContentOWSHTML,MagazineNumber'&sortlist='NumberPublishDateOWSDATE:descending'&rowlimit=40";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                string response = await httpClient.GetStringAsync(queryUrl);

                JObject body = JObject.Parse(response);
                var results = body["d"]["query"]["PrimaryQueryResult"]["RelevantResults"]["Table"]["Rows"]["results"];

                var numeros = new List<Model.Revista>();
                foreach (var result in results)
                {
                    var magazine = new Model.Revista
                    {
                        Titulo = result["Cells"]["results"][5]["Value"].ToString().Replace("CompartiMOSS\r\n            \r\n            \r\n            ", ""),
                        Editorial = result["Cells"]["results"][6]["Value"].ToString(),
                        UrlPDF = result["Cells"]["results"][4]["Value"].ToString(),
                        Numero = result["Cells"]["results"][7]["Value"].ToString(),
                        UrlPortada = result["Cells"]["results"][2]["Value"].ToString()
                    };

                    var regex = Regex.Match(magazine.UrlPortada, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase);
                    if (regex.Success)
                        magazine.UrlPortada = "http://www.compartimoss.com" + regex.Groups[1].Value;

                    numeros.Add(magazine);
                }

                return numeros;
            }
            catch (Exception e)
            {
                throw new Exception("Se ha producido un error en la carga de los números");
            }
        }

    }
}
