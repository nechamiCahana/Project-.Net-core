using Microsoft.EntityFrameworkCore;
using DAL.Interface;
using DAL.Data;
using MODELS.Models;
using BL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL.Dtos;
using Serilog;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using DAL.Profiles;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using AutoMapper;

namespace FinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture);

            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //jwt
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtKey = builder.Configuration["Jwt:Key"];

           
            if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("JWT settings are not configured properly.");
            }


            builder.Services.AddAuthentication(options =>
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
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });


            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerGen(op =>
            {
                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                op.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                             Id="Bearer"
                          }
                    },
                    new string[]{}
                }
            });
            });



            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));
            builder.Services.AddScoped<IBooklet, BookletData>();
            builder.Services.AddScoped<IOrder, OrderData>();
            builder.Services.AddScoped<IManager, ManagerData>();
            builder.Services.AddScoped<IUser, UserData>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(BookletProfile).Assembly);


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // SeedData is not needed if the user is creating the first manager
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<LogMiddlware>();
            //app.UseMiddleware<JwtMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
