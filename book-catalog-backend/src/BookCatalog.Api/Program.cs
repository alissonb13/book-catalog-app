using System.Text.Json;
using System.Text.Json.Serialization;
using BookCatalog.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.OpenApi.Models;

const string allowViteAppPolicy = "AllowViteApp";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowViteAppPolicy,
        policy => { policy.WithOrigins("http://localhost:5173", "http://localhost:8080").AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddDomainDependencies();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Book Catalog API",
            Description = "This API returns a book catalog and allows filter and search them",
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseCors(allowViteAppPolicy);
app.Run();