using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreParser.Models
{
    public class ModelGood
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public decimal Price { get; set; }

        public int[] GoodIds { get; set; }
    }
}