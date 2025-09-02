using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Repositories;
using BookCatalog.Infrastructure.Data.Configurations;
using BookCatalog.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace BookCatalog.Infrastructure.DependencyInjection;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BookCatalogDbContext>(options =>
            options.UseNpgsql(
                    new NpgsqlDataSourceBuilder(
                            configuration.GetConnectionString("BookCatalogConnectionString")
                        )
                        .EnableDynamicJson()
                        .Build()
                )
                .UseSeeding((context, _) =>
                {
                    CreateBooks(context);
                    context.SaveChanges();
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    CreateBooks(context);
                    await context.SaveChangesAsync(cancellationToken);
                })
        );

        services.AddScoped<IBooksRepository, BooksRepository>();

        return services;
    }

    private static void CreateBooks(DbContext context)
    {
        var contextSetBooks = context.Set<Book>();
        var existingBooks = contextSetBooks.OrderBy(x => x.CreatedAt).FirstOrDefault();

        if (existingBooks is null)
        {
            contextSetBooks.AddRangeAsync(BooksSeeding.GenerateBooks());
        }
    }
}