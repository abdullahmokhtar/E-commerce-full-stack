using Backend.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductSubCategory>().HasKey(ps => new { ps.ProductId, ps.SubCategoryId });

            modelBuilder.Entity<Product>().Property(e => e.Images)
                .HasConversion(
                v => string.Join(',', v),
                v=> v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<UserResetCode>().HasKey(e => new { e.AppUserId, e.Code });
            modelBuilder.Entity<UserResetCode>()
                .HasOne(e => e.AppUser)
                .WithOne(e => e.UserResetCode)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WishList>().HasKey(e => new { e.AppUserId, e.ProductId });

            modelBuilder.Entity<Cart>()
                .HasOne(e => e.AppUser)
                .WithOne(e => e.Cart)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartProduct>().HasKey(e => new { e.ProductId, e.CartId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(e => e.Product)
                .WithMany(e => e.CartProducts)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartProduct>()
                .HasOne(e => e.Cart)
                .WithMany(e => e.CartProducts)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(e => e.AppUser)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().OwnsOne(o => o.ShippingAddress, x => x.WithOwner());

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserResetCode> UserResetCodes { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
