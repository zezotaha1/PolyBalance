using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolyBalance.Models;
using PolyBalance.Services.parties;
using PolyBalance.Services;
using PolyBalance.Repository;
using PolyBalance.Services.PartyTypes;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Configure DbContext
        builder.Services.AddDbContext<PolyBalanceDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

        builder.Services.AddControllers();

        // Swagger setup
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Dependency Injection
        builder.Services.AddScoped<Validation>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IPartiesServices, PartiesServices>();
        builder.Services.AddScoped<IPartyTypesServices, PartyTypesServices>();

        // Add CORS configuration
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // Enable CORS globally
        app.UseCors("AllowAnyOrigin");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
