using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<User> //DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { }


        // add your models here
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<UserStock> UserStocks { get; set; }

        // public DbSet<User> Users { get; set; }

        // make sure at least one role for a user is out there.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserStock>()
            .HasKey(us => new { us.UserId, us.StockId });

            modelBuilder.Entity<UserStock>()
                .HasOne(u => u.User) // One-to-many relationship with User
                .WithMany(u => u.UserStocks) // Multiple user relationships
                .HasForeignKey(u => u.UserId); // Only one user relationship

            modelBuilder.Entity<UserStock>()
               .HasOne(s => s.Stock) // One-to-many relationship with User
               .WithMany(s => s.UserStocks) // Multiple user relationships
               .HasForeignKey(s => s.StockId); // Only one user relationship


            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "user",
                    NormalizedName = "USER"
                }


            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

