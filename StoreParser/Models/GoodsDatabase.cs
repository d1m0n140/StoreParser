namespace StoreParser.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GoodsDatabase : DbContext
    {
        public GoodsDatabase()
            : base("name=GoodsDatabase")
        {
        }

        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Good>()
                .HasMany(e => e.Images)
                .WithRequired(e => e.Good)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Good>()
                .HasMany(e => e.Prices)
                .WithRequired(e => e.Good)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Price>()
                .Property(e => e.Cost)
                .HasPrecision(18, 0);
        }
    }
}
