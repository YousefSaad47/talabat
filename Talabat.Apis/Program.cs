namespace Talabat.Apis;

public class Program
{
    public static void Main(string[] args)
    {
        #region Configure Service

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the DI container.

        builder.Services.AddControllers(); // Register Required WebApis Services to DI Container
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #endregion

        var app = builder.Build();

        #region Configure Kestrel Middlewares

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        #endregion

        app.Run();
    }
}