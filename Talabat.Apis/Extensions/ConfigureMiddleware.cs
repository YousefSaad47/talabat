using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Middlewares;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Apis.Extensions;

public static class ConfigureMiddleware
{
    public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
    {
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
        
        return app;
    }
}