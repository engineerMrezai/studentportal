using Microsoft.EntityFrameworkCore;
using StudentPortal.Models.Entieis;

namespace StudentPortal.Data;

public class AplicationDbContext: DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) :base(options)
    {
        
    }

    public DbSet<students> Students { get; set; }
}