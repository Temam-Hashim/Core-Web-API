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

        // public DbSet<User> Users { get; set; }

        // make sure at least one role for a user is out there.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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



// protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//     modelBuilder.Entity<Comment>()
//         .HasOne(c => c.Stock) // One-to-many relationship with Stock
//         .WithMany(s => s.Comments) // One Stock can have many Comments
//         .HasForeignKey(c => c.StockId) // Foreign key in Comment
//         .OnDelete(DeleteBehavior.SetNull); // Define the delete behavior (optional)
// }