namespace StoreParser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        public int Id { get; set; }

        public decimal Cost { get; set; }

        public int GoodId { get; set; }

        [Required]
        public System.DateTime DateTime { get; set; }

        public virtual Good Good { get; set; }
    }
}
