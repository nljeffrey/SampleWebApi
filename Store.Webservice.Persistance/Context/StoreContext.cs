using System;
using Microsoft.EntityFrameworkCore;
using Store.Webservice.Domain.Entities;

namespace Store.Webservice.Persistence.Context
{
    /// <summary>
    /// The Store context.
    /// </summary>
    public class StoreContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreContext" /> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}