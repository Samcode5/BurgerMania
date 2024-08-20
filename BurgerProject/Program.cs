
using BurgerProject.Data;
using BurgerProject.Mappings;
using Microsoft.EntityFrameworkCore;
using BurgerProject.Repositories;
using BurgerProject.DTOs;
using BurgerProject.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace BurgerProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            //Auto mapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IBurgerTypeRepo, BurgerTypeRepo>();
            builder.Services.AddScoped<IBurgerRepo, BurgerRepo>();
            builder.Services.AddScoped<IOrderRepo,OrderRepo>();





            //builder.Services.AddControllers();
            builder.Services.AddControllers(options =>
        options.RespectBrowserAcceptHeader = true
            ).AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
           // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen((options) =>
            {
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                                            new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]
                    {

                    }
                    }

                });
            });

            JwtSettings jwtSettings = new JwtSettings();
            //to bind properties of JwtSettings
            builder.Configuration.Bind("JwtSettings", jwtSettings);

            //register the 
            builder.Services.AddSingleton(jwtSettings);
            // JWT Authentication
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey); // Use a strong secret key
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(configurePolicy =>
            {
                configurePolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                app.UseHttpsRedirection();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
