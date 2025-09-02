using System.Reflection;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Infrastructure.Data.Configurations;

public class BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}