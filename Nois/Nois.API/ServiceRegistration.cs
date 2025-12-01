using Nois.Application.Interfaces;
using Nois.Application.Profiles;
using Nois.Application.Services;
using Nois.Persistance.Contexts;
using Nois.Persistance.Repositories;
using Serilog;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Nois.Application.Validators.CategoryValidators;
using Nois.Domain.Interfaces;
using Nois.Infrastructure.Services;

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
            services.AddSwaggerGen();

            services.AddDbContext<NoisDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfile());
            });

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();

            //Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBlobStorageService,BlobStorageService>();
            services.AddScoped<IProductVariantService, ProductVariantService>();
        }
    }
}
