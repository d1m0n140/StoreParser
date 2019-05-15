using AngleSharp.Html.Dom;
using StoreParser.Models;
using System.Collections.Generic;

namespace StoreParser.ParserCore
{
    interface IParser
    {
        List<Good> ParseLinks(IHtmlDocument htmlDoc);

        decimal ParsePrice(IHtmlDocument htmlDoc);

        string ParseImageLink(IHtmlDocument htmlDoc);

        string ParseDescription(IHtmlDocument htmlDoc);
    }
}