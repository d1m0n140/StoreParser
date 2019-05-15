using StoreParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreParser.ParserCore
{
    class ParserWorker
    {
        IParser parser;
        ParserSettings parserSettings;
        HtmlLoader loader;
        bool isActive;

        public bool IsActive
        {
            get { return isActive; }
        }

        public IParser Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public ParserSettings ParserSettings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value;
            }
        }

        public ParserWorker(IParser parser)
        {
            loader = new HtmlLoader();
            this.parser = parser;
        }

        public ParserWorker(IParser parser, ParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public async Task<bool> StartParsing()
        {
            bool result = false;
            if (!isActive)
            {
                isActive = true;
                result = await Worker();
            }
            return result;
        }

        public void Abort()
        {
            isActive = false;
        }

        private async Task<bool> Worker()
        {
            bool parsingResult = false;
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    //OnLinksCompleted?.Invoke(this);
                    return parsingResult;
                }
                string link = parserSettings.BaseUrl + parserSettings.Prefix.Replace("{CurrentPage}", i.ToString());

                var document = await loader.GetPageSourceByLink(link);
                if (document != null)
                {
                    List<Good> result = parser.ParseLinks(document);
                    
                    var database = new DbMediator();
                    foreach (var good in result)
                    {
                        good.Link = parserSettings.UrlHead + good.Link;
                        document = await loader.GetPageSourceByLink(good.Link);
                        if (document != null)
                        {
                            List<Good> goodsDb = database.GetGoodsByRef(good.Link);
                            int goodId;
                            if (goodsDb.Count < 1)
                            {
                                string description = parser.ParseDescription(document);
                                string imgLink = parser.ParseImageLink(document);
                                good.Description = description;
                                goodId = database.InsertGood(good);
                                byte[] img = await loader.GetImage(imgLink);
                                Image image = new Image()
                                {
                                    GoodId = goodId,
                                    ImageLink = imgLink,
                                    Img = img
                                };
                                database.InsertImage(image);
                            }
                            else
                            {
                                goodId = goodsDb.ToArray()[0].Id;
                            }
                            decimal cost = parser.ParsePrice(document);
                            Price price = new Price()
                            {
                                GoodId = goodId,
                                Cost = cost,
                                DateTime = System.DateTime.Now
                            };
                            database.InsertPrice(price);
                        }
                    }
                    if (result.Count > 0)
                    {
                        parsingResult = true;
                    }
                }
            }
            isActive = false;
            return parsingResult;
        }

    }
}