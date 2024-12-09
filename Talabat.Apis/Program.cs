using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Errors;
using Talabat.Apis.Extensions;
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

        builder.Services.AddApplicationServices(builder.Configuration);
        
        #endregion

        var app = builder.Build();
        
        await app.ConfigureMiddlewaresAsync();
        
        #region Configure Kestrel Middlewares

       

        #endregion

        app.Run();
    }
}