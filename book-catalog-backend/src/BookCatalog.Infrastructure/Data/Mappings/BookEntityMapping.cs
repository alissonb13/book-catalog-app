using BookCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCatalog.Infrastructure.Data.Mappings;

public class BookEntityMapping : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.ISBN)
            .HasColumnName("isbn")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Author)
            .HasColumnName("author")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Ownership)
            .HasColumnName("ownership")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.Id)
            .HasColumnName("id");
        
        builder.HasIndex(x => x.ISBN)
            .IsUnique();

        builder.HasKey(x => x.Id);

        builder.ToTable("books");
    }
}