using BlazorMinimalAPI.Data_Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
    
    }
    
    public DbSet<Person> People { get; set; }

}
