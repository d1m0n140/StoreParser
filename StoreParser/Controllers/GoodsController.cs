using System;
using System.Web.Http;
using StoreParser.Models;

namespace StoreParser.Controllers
{
    public class GoodsController : ApiController
    {
        [Route("api/goods/{goodId}")]
        public ModelGood Get(int goodId)
        {
            DbMediator database = new DbMediator();
            var responce = database.GetModelGood(goodId);
            return responce;
        }
    }
}
