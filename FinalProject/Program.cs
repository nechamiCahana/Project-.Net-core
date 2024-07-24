using Microsoft.EntityFrameworkCore;
using DAL.Interface;
using DAL.Data;
using MODELS.Models;
using BL;
using AutoMapper.Extensions.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL.Dtos;
using Serilog;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using DAL.Profiles;

namespace FinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
        //    string myCors = "_myCors";
        //    // Setup Serilog to write to a file
        //    Log.Logger = new LoggerConfiguration()
        //.WriteTo.File(@"C:\git_proj_.netcore\project_.net_core\project_.net_core\myLogDoc.txt",
        //rollingInterval: RollingInterval.Day)
        //.CreateLogger();
        //    var builder = WebApplication.CreateBuilder(args);

        //    builder.Services.AddControllers();
        //    builder.Services.AddEndpointsApiExplorer();



        //    builder.Services.AddCors(op =>
        //    {
        //        op.AddPolicy(myCors,
        //            builder =>
        //            {
        //                builder.WithOrigins("*")
        //                .AllowAnyHeader()
        //                .AllowAnyMethod();
        //            });
        //    });

            //jwt
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtKey = builder.Configuration["Jwt:Key"];

           
            if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("JWT settings are not configured properly.");
            }


            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        ValidAudience = builder.Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            //        RoleClaimType = ClaimTypes.Role
            //    };
            //});

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

            //builder.Services.AddAuthorization();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //builder.Services.AddAutoMapper(typeof(BookletProfile).Assembly);
            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));
            builder.Services.AddScoped<IBooklet, BookletData>();
            builder.Services.AddScoped<IOrder, OrderData>();
            builder.Services.AddScoped<IManager, ManagerData>();
            builder.Services.AddScoped<IUser, UserData>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<LogMiddlware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
