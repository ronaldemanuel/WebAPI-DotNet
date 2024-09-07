using Microsoft.EntityFrameworkCore;
using WebAPI_8.Models;

namespace WebAPI_8.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<AuthorModel> Authors { get; set; }
    public DbSet<BookModel> Books { get; set; }
}