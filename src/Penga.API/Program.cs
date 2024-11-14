
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Penga.API.Middlewares;
using Penga.Application;
using Penga.Application.Features.Costs;
using Penga.Infrastructure;
using System.Globalization;

namespace PENGA.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<PengaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PengaDb")));

            var useAuthorization = !builder.Environment.IsDevelopment();
            if (useAuthorization)
            {
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(configuration.GetSection("EntraID"));
            }

            builder.Services.AddValidatorsFromAssemblyContaining<AddCostCategory>();
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            FeatureRegistrar.Register(app, builder =>
            {
                if (useAuthorization)
                {
                    builder
                        .WithOpenApi()
                        .RequireAuthorization();
                }
                else
                {
                    builder
                        .WithOpenApi();
                }                
            });
            app.Run();
        }
    }
}
