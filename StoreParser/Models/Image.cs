namespace StoreParser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Img { get; set; }

        public int GoodId { get; set; }

        [Required]
        public string ImageLink { get; set; }

        public virtual Good Good { get; set; }
    }
}
