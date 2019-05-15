using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;

namespace StoreParser.ParserCore
{
    class HtmlLoader
    {
        private readonly HttpClient client;

        public HtmlLoader()
        {
            client = new HttpClient();
        }

        public async Task<IHtmlDocument> GetPageSourceByLink(string link)
        {
            IHtmlDocument document = null;
            try
            {
                var responce = await client.GetAsync(link);
                string page = null;
                if (responce != null && responce.StatusCode == HttpStatusCode.OK)
                {
                    page = await responce.Content.ReadAsStringAsync();
                }
                var domParser = new HtmlParser();
                document = await domParser.ParseDocumentAsync(page);
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return document;
        }

        public async Task<byte[]> GetImage(string link)
        {
            byte[] image = null;
            try
            {
                var responce = await client.GetAsync(link);
                if (responce != null && responce.StatusCode == HttpStatusCode.OK)
                {
                    image = await responce.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return image;
        }
    }
}