namespace LeCiel;

using System.Text;
using LeCiel.Database;
using LeCiel.Database.Models;
using LeCiel.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public static class Program
{
    private static WebApplicationBuilder builder = null!;
    private static WebApplication app = null!;

    public static void Main(string[] args)
    {
        BuildBuilder(args);
        AddServices();
        BuildApplication();
        UseMiddlewares();
        app.Run();
    }

    private static void BuildBuilder(string[] args)
    {
        builder = WebApplication.CreateBuilder(args);
    }

    private static void BuildApplication()
    {
        app = builder.Build();
    }

    private static void AddServices()
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        AddSwagger();
        AddEntity();
        AddIdentity();
        AddJwtBearer();
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<ProductsRepository>();
        builder.Services.AddScoped<CategoriesRepository>();
        builder.Services.AddScoped<TagsRepository>();
    }

    private static void AddSwagger()
    {
        builder.Services.AddSwaggerGen();
    }

    private static void AddJwtBearer()
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });
    }

    private static void AddIdentity()
    {
        builder
            .Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddEntity()
    {
        var cs = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<AppContext>(options =>
        {
            var serverVersion = ServerVersion.AutoDetect(cs);
            options.UseMySql(cs, serverVersion);
        });
    }

    private static void UseMiddlewares()
    {
        UseSwagger();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.MapControllers();
    }

    private static void UseSwagger()
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
