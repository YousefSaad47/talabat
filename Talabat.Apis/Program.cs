using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Errors;
using Talabat.Apis.Middlewares;
using Talabat.Core;
using Talabat.Core.Mapping.Products;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.Services.Products;

namespace Talabat.Apis;

public class Program
{
    public static async Task Main(string[] args)
    {
        
        #region Configure Service
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the DI container.

        builder.Services.AddControllers(); // Register Required WebApis Services to DI Container
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(m => m.AddProfile(new ProductProfile(builder.Configuration)));

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(e => e.ErrorMessage);

                var validationErrorResponse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };
                
                return new BadRequestObjectResult(validationErrorResponse);
            };
        });

        #endregion

        var app = builder.Build();
        
        // Create Scope
        using var scope = app.Services.CreateScope();
        
        var services = scope.ServiceProvider;

        var _dbContext =  services.GetRequiredService<StoreContext>(); // Ask CLR for Creating Object From DbContext Explicitly 
        
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            
        try
        {
          await _dbContext.Database.MigrateAsync(); // Update Database  
          await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during migration");
        }

        #region Configure Kestrel Middlewares

        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.MapControllers();

        #endregion

        app.Run();
    }
}