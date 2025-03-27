using Infraestructure.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    
    public DbSet<CardEntity> PermissionEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}