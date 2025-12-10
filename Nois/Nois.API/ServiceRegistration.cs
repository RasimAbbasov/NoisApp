using Nois.Application.Interfaces;
using Nois.Application.Profiles;
using Nois.Application.Services;
using Nois.Persistance.Contexts;
using Nois.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Nois.Application.Validators.CategoryValidators;
using Nois.Domain.Interfaces;
using Nois.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Nois.Domain.Entities.Identity;
using System.Text;
using Nois.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Nois.Application.Exceptions;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Nois.API
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration) 
        {
            // Add services to the container.

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new() { Title = "Nois API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your token}",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
             }
             });
            });

            services.AddDbContext<NoisDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Lockout.AllowedForNewUsers = true;


                opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddEntityFrameworkStores<NoisDbContext>().AddDefaultTokenProviders();

            // JWT Auth

            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<JwtOptions>(jwtSection);
            //Email Settings
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));


            var jwtOptions = jwtSection.Get<JwtOptions>();
            var key = Encoding.UTF8.GetBytes(jwtOptions.Key);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero //adds extra time to existing lifetime of access token
                };
            });

        

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IProductStockRepository, ProductStockRepository>();


            //Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBlobStorageService,BlobStorageService>();
            services.AddScoped<IProductVariantService, ProductVariantService>();
            services.AddScoped<IProductStockService, ProductStockService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
