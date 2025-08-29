namespace LeCiel;

using LeCiel.Database;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    private static WebApplicationBuilder builder = null!;

    public static void Main(string[] args)
    {
        builder = WebApplication.CreateBuilder(args);
        ConfigureServices();
        var app = builder.Build();
        ConfigureMiddleware(app);
        app.Run();
    }

    private static void ConfigureServices()
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        AddAppContext();
    }

    private static void AddAppContext()
    {
        var cs = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<AppContext>(options =>
        {
            var serverVersion = ServerVersion.AutoDetect(cs);
            options.UseMySql(cs, serverVersion);
        });
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
    }
}
