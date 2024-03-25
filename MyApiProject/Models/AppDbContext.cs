using Microsoft.EntityFrameworkCore;
using MyApiProject.Models;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
