using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    
    public DbSet<UserEntity> UserEntity { get; set; }
    public DbSet<CardEntity> CardEntity { get; set; }
    public DbSet<TransactionEntity> TransactionEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasMany(x => x.Cards)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<CardEntity>()
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Card)
            .HasForeignKey(x => x.CardId);
    }
}