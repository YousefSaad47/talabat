using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.Context;

namespace Talabat.Apis.Extensions;

public static class ConfigureMiddleware
{
    public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
    {
        // Create Scope
        using var scope = app.Services.CreateScope();
        
        var services = scope.ServiceProvider;

        var _dbContext =  services.GetRequiredService<StoreContext>(); // Ask CLR for Creating Object From DbContext Explicitly 
        var _identityDbContext = services.GetRequiredService<StoreIdentityDbContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            
        try
        {
          await _dbContext.Database.MigrateAsync(); // Update Database  
          await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding
          await _identityDbContext.Database.MigrateAsync();
          await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.MapControllers();
        
        return app;
    }
}