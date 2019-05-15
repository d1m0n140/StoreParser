using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using StoreParser.ParserCore;
using StoreParser.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace StoreParser.AlloUA
{
    public class AlloParser : IParser
    {
        public List<Good> ParseLinks(IHtmlDocument htmlDoc)
        {
            var links = new List<Good>();

            var items = htmlDoc.QuerySelectorAll("a").Where(item => item.ClassName != null &&
                item.ClassName.Contains("product-name multiple-lines-crop"));
                foreach (var item in items)
                {
                    string link = item.GetAttribute("href") ?? "N/A";
                    string title = item.GetAttribute("title" ?? "N/A");
                    var good = new Good()
                    {
                        Title = title,
                        Link = link
                    };
                    links.Add(good);
                }
            return links;
        }

        public decimal ParsePrice(IHtmlDocument htmlDoc)
        {
            decimal price;
            var priceContainer = htmlDoc.QuerySelectorAll("span").Where(item => item.ClassName != null &&
                item.ClassName == "price").First();
            string textPrice = priceContainer.TextContent ?? "0";
            textPrice = textPrice.Trim();
            textPrice = textPrice.Replace(Regex.Match(textPrice, @"[^0-9\.\,]").Value, "");
            //textPrice = textPrice.Replace(" ", "");
            //textPrice = Regex.Match(textPrice, @"[0-9]*[\.|\,]?[0-9]*").Value;
            price = decimal.Parse(textPrice);
            return price;
        }

        public string ParseDescription(IHtmlDocument htmlDoc)
        {
            string description = null; //= "N/A";
            var items = htmlDoc.QuerySelectorAll("p").Where(item => item.ClassName != null &&
                    item.ClassName.Contains("text_general"));
            //if(items.Count() > 0)
            //{
            //    description = "";
            //}
            foreach(var item in items)
            {
                description += item.TextContent ?? "";
            }
            return description;
        }

        public string ParseImageLink(IHtmlDocument htmlDoc)
        {
            string imgLink;
            var imgLinkTag = htmlDoc.QuerySelectorAll("img").Where(item => item.Id != null &&
                item.Id.Contains("image")).First();
            imgLink = imgLinkTag.GetAttribute("src") ?? "N/A";
            return imgLink;
        }
    }
}