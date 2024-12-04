using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { }


        // add your models here
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stock> Stocks { get; set; }
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