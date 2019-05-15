using StoreParser.AlloUA;
using StoreParser.ParserCore;
using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace StoreParser.Controllers
{
    public class ParserController : ApiController
    {
        // GET api/parser
        //[Route("api/parser/{startPage}/{endPage}")]
        //public async Task<IEnumerable<string>> Get(int startPage, int endPage)
        [HttpPost]
        [Route("api/parser")]
        public async Task<IEnumerable<string>> Post([FromBody]FormDataCollection form)
        {
            int startPage = int.Parse(form.Get("startPage"));
            int endPage = int.Parse(form.Get("endPage"));
            ParserWorker parser = new ParserWorker
                (
                    new AlloParser(),
                    new ParserSettings()
                    {
                        BaseUrl = "https://allo.ua/ua/products/mobile/klass-kommunikator_smartfon/",
                        Prefix = "p-{CurrentPage}/",
                        UrlHead = "https:",
                        StartPoint = startPage,
                        EndPoint = endPage
                    }
                );
            bool parsingResult = await parser.StartParsing();
            string result = parsingResult ? "Success" : "Failed";
            return new string[] { $"Parsing finished. Result: " + result, DateTime.Now.ToString() };
        }
    }
}