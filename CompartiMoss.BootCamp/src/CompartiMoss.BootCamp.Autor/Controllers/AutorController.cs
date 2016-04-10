using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CompartiMoss.BootCamp.Autor.Controllers
{
    [Route("api/[controller]")]
    public class AutorController : Controller
    {
    
        private const string url = "http://www.compartimoss.com/_api/search/query?querytext=##query##&QueryTemplatePropertiesUrl='spfile://webroot/queryparametertemplate.xml'&clienttype='Custom'";


        [HttpGet]
        public async Task<List<Model.Autor>> Get()
        {
            try
            {
                var queryUrl = url.Replace("##query##", "'ContentTypeId:0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF3900242457EFB8B24247815D688C526CD44D005BA4DEB48BBA154A8FFCC9390BF5C469*'");
                queryUrl += "&selectproperties='PublishingImage,Path,Url,Title,PublishingPageContentOWSHTML,TwitterOWSTEXT,BlogOWSLINK,JobTitle1OWSTEXT,ArticleAuthor'&sortlist='Title:ascending'&rowlimit=80";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                var response = await httpClient.GetStringAsync(queryUrl);

                JObject body = JObject.Parse(response);
                var results = body["d"]["query"]["PrimaryQueryResult"]["RelevantResults"]["Table"]["Rows"]["results"];

                var authors = new List<Model.Autor>();
                foreach (var result in results)
                {
                    var author = new Model.Autor()
                    {
                        Name = result["Cells"]["results"][5]["Value"].ToString().Replace("CompartiMOSS\r\n            \r\n            \r\n            ", ""),
                        Description = result["Cells"]["results"][6]["Value"].ToString(),
                        Twitter = result["Cells"]["results"][7]["Value"].ToString(),
                        NameBlog = result["Cells"]["results"][8]["Value"].ToString(),
                        JobTittle = result["Cells"]["results"][9]["Value"].ToString(),
                        Blog = result["Cells"]["results"][4]["Value"].ToString(),
                        Image = result["Cells"]["results"][2]["Value"].ToString()
                    };
                    var regex = Regex.Match(author.Image, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase);
                    if (regex.Success)
                        author.Image = "http://www.compartimoss.com" + regex.Groups[1].Value;

                    var regexBlog = Regex.Match(author.Blog, "<a.+?href=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase);
                    if (regexBlog.Success)
                        author.Blog = regexBlog.Groups[1].Value.Replace("&#58;", ":");

                    if (!string.IsNullOrEmpty(author.Twitter))
                        author.Twitter = author.Twitter.Replace("@", "http://twitter.com/");

                    authors.Add(author);
                }

                return authors;
            }
            catch (Exception e)
            {
                throw new Exception("Se ha producido un error en la carga de los autores");
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
