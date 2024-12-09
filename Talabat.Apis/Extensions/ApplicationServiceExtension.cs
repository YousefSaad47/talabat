using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Errors;
using Talabat.Core;
using Talabat.Core.Mapping.Products;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.Services.Products;

namespace Talabat.Apis.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBuiltInServices();
        services.AddSwaggerServices();
        services.AddDbContextServices(configuration);
        services.AddUserDefinedServices();
        services.AddAutoMapperServices(configuration);
        services.ConfigureInvalidStateResponseServices();
        return services;
    }
    
    private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
    {
        services.AddControllers(); // Register Required WebApis Services to DI Container
        return services;
    }
    
    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    
    private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
    
    private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
    {
        
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    
    private static IServiceCollection AddAutoMapperServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
        return services;
    }
    
    
    private static IServiceCollection ConfigureInvalidStateResponseServices(this IServiceCollection services)
    {
        
        services.Configure<ApiBehaviorOptions>(options =>
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
        return services;
    }
}