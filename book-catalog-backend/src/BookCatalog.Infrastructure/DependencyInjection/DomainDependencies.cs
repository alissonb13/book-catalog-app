using BookCatalog.Domain.Interfaces.Services;
using BookCatalog.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookCatalog.Infrastructure.DependencyInjection;

public static class DomainDependencies
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
    {
        return services.AddScoped<IBookService, BookService>();
    }
}