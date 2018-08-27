namespace TiendaMusical.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelMS : DbContext
    {
        public ModelMS()
            : base("name=ModelMS")
        {
        }

        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Albums>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Albums>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Albums)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Albums>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Albums)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artists>()
                .HasMany(e => e.Albums)
                .WithRequired(e => e.Artists)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Carts>()
                .Property(e => e.CartId)
                .IsUnicode(false);

            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Albums)
                .WithRequired(e => e.Genres)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetails>()
                .Property(e => e.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Total)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);
        }
    }
}
